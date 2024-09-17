using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public Action OnDestinationReached;
        private bool _isReached = true;

        [SerializeField] private MoveComponent moveComponent;

        private Vector2 _destination;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                OnDestinationReached?.Invoke();
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}