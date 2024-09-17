using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMovementController: MonoBehaviour
    {
        [SerializeField] private MovementInputManager _movementInputManager;

        [SerializeField] private MoveComponent _moveComponent;
        
        private void FixedUpdate()
        {
            _moveComponent.MoveByRigidbodyVelocity(new Vector2(_movementInputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}