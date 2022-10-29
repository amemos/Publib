using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowState : CState<TestCube>
{
    public YellowState(TestCube entity) : base(entity){ }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Entity.ChangeColor(Colors.Yellow);
        SubscribeForOnTimeExecution(2f, () => { Entity.StateMachine.ChangeState(Entity.ColorSkipState); });
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
