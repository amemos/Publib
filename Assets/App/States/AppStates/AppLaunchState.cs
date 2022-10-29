using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLaunchState : CState<AppManager>
{
    public AppLaunchState(AppManager entity) : base(entity) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        // Analytics and other 3rd parties will be initiated here.

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

