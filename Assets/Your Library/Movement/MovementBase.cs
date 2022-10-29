using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using yout.library.globals;

namespace your.library
{
    public class MovementBase : MonoBehaviour, IMovement
    {
        public Action OnMoveStarted;
        public Action OnMoveCompleted;
        public Action OnMoveUpdate;
        public Action OnMoveFixedUpdate;
        public Action<float> OnMoveTime;

        private Transform _transform;
        private Rigidbody _rigidbody;
        private NavMeshAgent _agent;


        void Start()
        {
            _transform = transform;
            TryGetComponent<Rigidbody>(out _rigidbody);
            TryGetComponent<NavMeshAgent>(out _agent);

        }
        public void Move(Vector3 targetPosition, float time, MoveType moveType = MoveType.Transform, EaseType easeType = EaseType.Linear)
        {
            if(EvaluateMoveTypeChoice(moveType))
            {

            }
        }

        void IMovement.OnMoveCompleted()
        {
            throw new NotImplementedException();
        }

        void IMovement.OnMoveFixedUpdate()
        {
            throw new NotImplementedException();
        }

        void IMovement.OnMoveStarted()
        {
            throw new NotImplementedException();
        }

        void IMovement.OnMoveTime(float elapsed_time)
        {
            throw new NotImplementedException();
        }

        void IMovement.OnMoveUpdate()
        {
            throw new NotImplementedException();
        }

        private bool EvaluateMoveTypeChoice(MoveType moveType)
        {
            bool valid = true;
            switch (moveType)
            {
                case MoveType.Transform:
                    break;
                case MoveType.Physic:
                    valid = _rigidbody != null;
                    break;
                case MoveType.NavMesh:
                    valid = _agent != null;
                    break;
                case MoveType.DoTween:
                    
                    break;
                default:
                    break;
            }

            return valid;
        }



        void Update()
        {

        }
    }



}
