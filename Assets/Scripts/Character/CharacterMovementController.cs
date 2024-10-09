using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMovementController : Listeners.IFixUpdaterListener
    {
        private readonly MoveComponent _moveComponent;
        private MovementInputManager _movementInputManager;

        public CharacterMovementController(MovementInputManager movementInputManager, MoveComponent moveComponent)
        {
            _movementInputManager = movementInputManager;
            _moveComponent = moveComponent;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            _moveComponent.MoveByRigidbodyVelocity(
                new Vector2(_movementInputManager.HorizontalDirection, 0) * deltaTime);
        }
    }
}