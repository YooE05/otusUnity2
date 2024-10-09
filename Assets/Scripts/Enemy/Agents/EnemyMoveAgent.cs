using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : Listeners.IFixUpdaterListener
    {
        public Action OnDestinationReached;
        
        private bool _isReached = true;
        private Vector2 _destination;

        private readonly MoveComponent _moveComponent;
        private readonly Transform _enemyTransform;

        public EnemyMoveAgent(MoveComponent moveComponent, Transform enemyTransform)
        {
            _moveComponent = moveComponent;
            _enemyTransform = enemyTransform;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2) _enemyTransform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                OnDestinationReached?.Invoke();
                return;
            }

            var direction = vector.normalized * deltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}