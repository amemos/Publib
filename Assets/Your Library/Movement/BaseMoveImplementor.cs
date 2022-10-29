using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using your.library;
using yout.library.globals;

namespace your.library
{
    public abstract class BaseMoveImplementor : MonoBehaviour, IMovement
    {
        public Action OnStarted;
        public Action OnCompleted;
        public Action OnUpdate;
        public Action OnFixedUpdate;
        public Action<float> OnTime;
        public Action<float> OnDistance;

        protected float MoveStartTime;
        protected float ElapsedTime;
        protected bool IsMoving;

        public virtual void Move(Vector3 targetPosition, float time, MoveType moveType = MoveType.Transform, EaseType easeType = EaseType.Linear)
        {
            if (IsValidType(moveType))
            {
                MoveStartTime = Time.time;
                OnMoveStarted();
                IsMoving = true;
            }

        }

        public virtual bool IsValidType(MoveType moveType)
        {
            if (moveType == MoveType.DoTween || moveType == MoveType.NavMesh || moveType == MoveType.Physic || moveType == MoveType.Transform)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void OnMoveCompleted()
        {
            OnCompleted?.Invoke();
        }

        public virtual void OnMoveFixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        public virtual void OnMoveStarted()
        {
            OnStarted?.Invoke();
        }

        public virtual void OnMoveTime(float elapsed_time)
        {

        }

        public virtual void OnMoveUpdate()
        {
            OnUpdate?.Invoke();
        }


        public virtual void Awake()
        {

        }

        public virtual void Update()
        {
            OnMoveUpdate();
        }

        public virtual void FixedUpdate()
        {
            ElapsedTime = Time.time - MoveStartTime;
            OnMoveFixedUpdate();
        }
    }




    public class TransformBasedMove : BaseMoveImplementor
    {
        public override bool IsValidType(MoveType moveType)
        {
            return base.IsValidType(moveType);
        }
    }

    public class RigidbodyBasedMove : BaseMoveImplementor
    {
        public override bool IsValidType(MoveType moveType)
        {
            return base.IsValidType(moveType);
        }
    }

    public class NavMeshBasedMove : BaseMoveImplementor
    {
        public override bool IsValidType(MoveType moveType)
        {
            return base.IsValidType(moveType);
        }
    }

    public class DOTweenBasedMove : BaseMoveImplementor
    {
        public override bool IsValidType(MoveType moveType)
        {
            return base.IsValidType(moveType);
        }
    }
}
