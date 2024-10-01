using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMovementController: MonoBehaviour, Listeners.IFixUpdaterListener
    {
        [SerializeField] private MovementInputManager _movementInputManager;

        [SerializeField] private MoveComponent _moveComponent;
        
        public void OnFixedUpdate(float deltaTime)
        {
            _moveComponent.MoveByRigidbodyVelocity(new Vector2(_movementInputManager.HorizontalDirection, 0) * deltaTime);
        }
    }
}