using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yout.library.globals;

namespace your.library
{
    public interface IMovement
    {
        public void Move(Vector3 targetPosition, float time, MoveType moveType = MoveType.Transform, EaseType easeType = EaseType.Linear);

        public void OnMoveStarted();
        public void OnMoveCompleted();
        public void OnMoveUpdate();
        public void OnMoveFixedUpdate();
        public void OnMoveTime(float elapsed_time);
    }

}
