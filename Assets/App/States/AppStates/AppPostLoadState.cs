using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppPostLoadState : CState<AppManager>
{
    public AppPostLoadState(AppManager entity) : base(entity) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        //Game manager will be started.
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
