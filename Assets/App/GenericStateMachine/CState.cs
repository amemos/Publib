using UnityEngine;
using System;
using System.Collections.Generic;


public abstract class CState<T> : IState
{
    protected T Entity;

    private float OnStateEnterTime;
    private float OnStateExitTime;
    private bool IsTimeExecutionActive = false;
    private bool IsPeriodicExecutionActive = false;

    private List<OnTimeAction> TimeActionSet;
    private List<OnPeriodicAction> PeriodicActionSet;

    public Action<T> OnStateEnterAction;
    public Action<T> OnStateExitAction;

    private int TickCount = 0;
    private int TransitionIntoCount = 0;

    private EStateMachineTiming UpdateType;

    public abstract void PreTick();

    public void SetStateUpdateType(EStateMachineTiming update_type)
    {
        UpdateType = update_type;
    }

    /// <summary>
    /// Update of state.
    /// </summary>
    public virtual void Tick() 
    {
        if(TickCount == 0)
        {
            PreTick();
        }
        TickCount++;

        if(UpdateType == EStateMachineTiming.Update)
        {
            ProcessTimeActions();
            ProcessPeriodicActions();
        }
    }

    /// <summary>
    /// FixedUpdate of state.
    /// </summary>
    public virtual void TickPhysics()
    {
        if(UpdateType == EStateMachineTiming.FixedUpdate)
        {
            ProcessTimeActions();
            ProcessPeriodicActions();
        }
    }

    private void ProcessTimeActions()
    {
        if (IsTimeExecutionActive)
        {
            List<OnTimeAction> TimeActionCopySet = new List<OnTimeAction>(TimeActionSet);
            foreach (var item in TimeActionCopySet)
            {
                if (!item.Executed)
                {
                    if (GetElapsedTimeOnState() > item.ExecutionTime)
                    {
                        item.Execution?.Invoke();
                        item.Executed = true;
                        if (item.AutoUnsubscribe)
                        {
                            UnSubscribeForOnTimeExecution(item);
                        }
                    }
                }
            }
        }
    }

    private void ProcessPeriodicActions()
    {
        if (IsPeriodicExecutionActive)
        {
            List<OnPeriodicAction> PeriodicActionCopySet = new List<OnPeriodicAction>(PeriodicActionSet);
            foreach (var item in PeriodicActionCopySet)
            {
                float current_execution_time = item.Period * item.SequenceCount;

                if (!item.IsPlaying)
                {
                    UnSubscribeForPeriodicExecution(item);
                    continue;
                }

                if (GetElapsedTimeOnState() > current_execution_time && item.IsPlaying)
                {
                    item.Execution?.Invoke();
                    item.SequenceCount++;
                }
            }
        }
    }

    /// <summary>
    /// It is executed on entrance of state.
    /// </summary>
    public virtual void OnStateEnter() 
    { 
        OnStateEnterTime = Time.time;
        TickCount = 0;
        TransitionIntoCount++;
        OnStateEnterAction?.Invoke(Entity);
    }

    /// <summary>
    /// It is executed on exit of state.
    /// </summary>
    public virtual void OnStateExit() 
    { 
        OnStateExitTime = Time.time;
        IsTimeExecutionActive = false;
        IsPeriodicExecutionActive = false;
        OnStateExitAction?.Invoke(Entity);
    }

    public CState(T t)
    {
        this.Entity = t;
        TimeActionSet = new List<OnTimeAction>();
        PeriodicActionSet = new List<OnPeriodicAction>();
    }

    public float GetElapsedTimeOnState()
    {
        return Time.time - OnStateEnterTime;
    }

    public float GetOnStateEnterTime()
    {
        return OnStateEnterTime;
    }

    public float GetOnStateExitTime()
    {
        return OnStateExitTime;
    }

    public int GetTransitionIntoCount()
    {
        return TransitionIntoCount;
    }

    public int GetTimeActionCount()
    {
        return TimeActionSet.Count;
    }

    public int GetPeriodicActionCount()
    {
        return PeriodicActionSet.Count;
    }

    /// <summary>
    /// It is executed on time during state.
    /// return : Count of subscribed actions.
    /// </summary>
    public object SubscribeForOnTimeExecution(float on_time, Action action, bool auto_unsubscribe = true)
    {
        IsTimeExecutionActive = true;
        OnTimeAction time_action = new OnTimeAction(action, on_time, auto_unsubscribe);
        TimeActionSet.Add(time_action);

        return time_action;
    }

    /// <summary>
    /// It is executed on time during state.
    /// return : Count of subscribed actions.
    /// </summary>
    public int UnSubscribeForOnTimeExecution(object obj)
    {
        TimeActionSet.Remove((OnTimeAction)obj);

        if(TimeActionSet.Count == 0)
        {
            IsTimeExecutionActive = false;
        }

        return TimeActionSet.Count;
    }

    /// <summary>
    /// It is executed on period during state.
    /// return : Count of subscribed actions.
    /// </summary>
    public object SubscribeForPeriodicExecution(float period_in_sec, Action action)
    {
        IsPeriodicExecutionActive = true;
        OnPeriodicAction periodic_action = new OnPeriodicAction(action, period_in_sec);
        PeriodicActionSet.Add(periodic_action);

        return periodic_action;
    }

    /// <summary>
    /// It is executed on period during state.
    /// return : Count of subscribed actions.
    /// </summary>
    public int UnSubscribeForPeriodicExecution(object obj)
    {
        PeriodicActionSet.Remove((OnPeriodicAction)obj);

        if (PeriodicActionSet.Count == 0)
        {
            IsPeriodicExecutionActive = false;
        }

        return PeriodicActionSet.Count;
    }



    public class OnTimeAction
    {
        public Action Execution;
        public float ExecutionTime;
        public bool Executed;
        public bool AutoUnsubscribe;

        public OnTimeAction(Action action, float on_time, bool auto_unsubscribe)
        {
            Execution = action;
            ExecutionTime = on_time;
            Executed = false;
            AutoUnsubscribe = auto_unsubscribe;
        }

        public void Reset()
        {
            Executed = false;
        }
    }

    public class OnPeriodicAction
    {
        public Action Execution;
        public float Period;
        public int SequenceCount;
        public bool IsPlaying;

        public OnPeriodicAction(Action action, float on_period)
        {
            Execution = action;
            Period = on_period;
            IsPlaying = true;
        }

        public void StopPeriodicExecution()
        {
            IsPlaying = false;
        }
    }


}


public interface IState
{
    public void PreTick();

    public void Tick();

    public void TickPhysics();

    public void OnStateEnter();

    public void OnStateExit();

    public float GetOnStateEnterTime();

    public float GetOnStateExitTime();

    public float GetElapsedTimeOnState();

    public void SetStateUpdateType(EStateMachineTiming update_type);

}
