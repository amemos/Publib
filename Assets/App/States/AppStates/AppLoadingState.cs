using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLoadingState : CState<AppManager>
{
    public AppLoadingState(AppManager entity) : base(entity) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        //Game assets will be pooled here.

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
