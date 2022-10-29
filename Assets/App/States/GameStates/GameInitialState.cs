using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialState : CState<GameManager>
{
    public GameInitialState(GameManager entity) : base(entity) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        //Game manager initial confiuration.
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
