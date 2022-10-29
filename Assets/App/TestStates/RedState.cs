using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedState : CState<TestCube>
{
    public RedState(TestCube entity) : base(entity) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Entity.ChangeColor(Colors.Red);

        OnTimeAction on_time_action = (OnTimeAction)SubscribeForOnTimeExecution(5f, () =>
        {
            Entity.StateMachine.ChangeState(Entity.YellowState);
        });
        
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void PreTick()
    {

    }
    public override void Tick()
    {
        base.Tick();
    }

    public override void TickPhysics()
    {
        base.TickPhysics();
    }



}
