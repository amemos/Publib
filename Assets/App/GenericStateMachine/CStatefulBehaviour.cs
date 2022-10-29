using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStatefulBehaviour : CStateMachineFactory
{

    [SerializeField] private List<string> StateLog;

    [SerializeField] private EStateMachineTiming StateMachineTime;

    protected override void Awake()
    {
        base.Awake();
        StateMachine.SetStateMachineUpdateType(StateMachineTime);
        StateLog = StateMachine.StateLog;
    }

}