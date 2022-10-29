
public class CStateMachineFactory : CFactoryBehaviour<CStateMachine<IState>>
{
    public CStateMachine<IState> StateMachine;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = ProducedItem;
    }

    public void RunStateMachine(IState InitialState)
    {
        StateMachine.StartWithState(InitialState);
    }

    virtual protected void Update()
    {
        StateMachine.CurrentState?.Tick();
    }

    virtual protected void FixedUpdate()
    {
        StateMachine.CurrentState?.TickPhysics();
    }

}