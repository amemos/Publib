using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using Object = System.Object;

public enum EStateMachineTiming
{
    Update,
    FixedUpdate
}
public class CStateMachine<T> where T : IState
{
    public EStateMachineTiming StateMachineTiming;

    public T CurrentState { get; private set; }
    private T PreviousState { get; set; }

    public int StateTransitionCount = 0;
    private bool TransitionSubscription = false;
    private List<OnTransitionAction> OnTransitionActionSet;

    public List<string> StateLog = new List<string>();

    public CStateMachine()
    {
        StateTransitionCount = 0;
        OnTransitionActionSet = new List<OnTransitionAction>();
    }

    public CStateMachine(T state) 
    {
        StateTransitionCount = 0;
        CurrentState = state;
        state.SetStateUpdateType(StateMachineTiming);
        state.OnStateEnter();
        OnTransitionActionSet = new List<OnTransitionAction>();
    }

    public void SetStateMachineUpdateType(EStateMachineTiming state_machine_timing)
    {
        StateMachineTiming = state_machine_timing;
    }

    public void StartWithState(T state)
    {
        KeepLog(state);

        StateTransitionCount = 0;
        CurrentState = state;
        state.SetStateUpdateType(StateMachineTiming);
        state.OnStateEnter();
    }

    public virtual void ChangeState(T new_state)
    {
        KeepLog(new_state);
        PreviousState = CurrentState;
        CurrentState.OnStateExit();

        if (TransitionSubscription)
        {
            int index = 0;
            OnTransitionAction[] on_transition_actions = new OnTransitionAction[OnTransitionActionSet.Count];
            on_transition_actions = OnTransitionActionSet.ToArray();

            foreach (var item in on_transition_actions)
            {
                bool from = item.FromState == null || item.FromState.GetType() == CurrentState.GetType();
                Component component = item.Component;
                if (new_state.GetType() == item.ToState.GetType() && from)
                {
                    try
                    {
                        item.OnTransition.Invoke(component);  
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("couldnt transition invoke : from " + CurrentState.ToString() + " to " + new_state.ToString()+ " " + ex.Message);
                    }

                    if (item.AutoUnsubscribe)
                    {
                        UnSubscribeForTransition(item);
                    }
                    index++;
                }
            }        

        }

        CurrentState = new_state;
        StateTransitionCount++;
        new_state.SetStateUpdateType(StateMachineTiming);
        new_state.OnStateEnter();

    }

    public T GetPreviousState()
    {
        return PreviousState;
    }

    public bool IsThatState(Type type)
    {
        return CurrentState.GetType() == type;
    }

    /// <summary>
    /// It is executed on certain state transition.
    /// If it is auto-unsubscribed, only first transition triggers the event.
    /// </summary>
    public Object SubscribeToStateTransition(IState to_state, Action<Component> action, Component component = null, bool AutoUnsubscribeAfterInvoke = false)
    {
        TransitionSubscription = true;
        OnTransitionAction transition_action = new OnTransitionAction(action, to_state, null, component, AutoUnsubscribeAfterInvoke);
        OnTransitionActionSet.Add(transition_action);
        return transition_action;
    }

    /// <summary>
    /// It is executed from certain to certain state transition.
    /// If it is auto-unsubscribed, only first transition triggers the event.
    /// </summary>
    public Object SubscribeToStateTransition(IState from_state, IState to_state, Action<Component> action, Component component = null, bool AutoUnsubscribeAfterInvoke = false)
    {
        TransitionSubscription = true;
        OnTransitionAction transition_action = new OnTransitionAction(action, to_state, from_state, component, AutoUnsubscribeAfterInvoke);
        OnTransitionActionSet.Add(transition_action);
        return transition_action;
    }


    public void UnSubscribeForTransition(Object obj)
    {
        OnTransitionActionSet.Remove((OnTransitionAction)obj);

        if(OnTransitionActionSet.Count == 0)
        {
            TransitionSubscription = false;
        }
    }

    private void KeepLog(T state)
    {
        if(state != null)
        {
            StateLog.Add(state.ToString() + " " + Time.time.ToString());
        }
    }


}


public class OnTransitionAction
{
    public Action<Component> OnTransition;
    public IState ToState;
    public IState FromState;
    public Component Component;
    public bool AutoUnsubscribe;

    public OnTransitionAction(Action<Component> action, IState to_state, IState from_state = null, Component component = null, bool AutoUnsubscribeAfterInvoke = false)
    {
        ToState = to_state;
        OnTransition = action;
        FromState = from_state;
        Component = component;
        AutoUnsubscribe = AutoUnsubscribeAfterInvoke;
    }
}