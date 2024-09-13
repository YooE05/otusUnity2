using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMovementController: MonoBehaviour
    {
        [SerializeField] private MovementInputManager movementInputManager;

        [SerializeField] private MoveComponent _moveComponent;
        
        private void FixedUpdate()
        {
            _moveComponent.MoveByRigidbodyVelocity(new Vector2(movementInputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}