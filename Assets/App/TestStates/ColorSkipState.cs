using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSkipState : CState<TestCube>
{
    public ColorSkipState(TestCube entity) : base(entity) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        SubscribeForPeriodicExecution(.5f, () => { Entity.ChangeColorNext(); });
        SubscribeForOnTimeExecution(10f, () => { Entity.StateMachine.ChangeState(Entity.RedState); });
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
