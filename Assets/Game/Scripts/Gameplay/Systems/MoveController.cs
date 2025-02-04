using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class MoveController : IFixedTickable
    {
        private readonly ICharacter _character;
        private readonly IMoveInput _moveInput;

        private bool _canMove;

        public MoveController(ICharacter character, IMoveInput moveInput)
        {
            _character = character;
            _moveInput = moveInput;
            _canMove = true;
        }

        public void SetMoveAbility(bool canMove)
        {
            _canMove = canMove;
        }

        void IFixedTickable.FixedTick()
        {
            if (!_canMove) return;

            _character.Move(_moveInput.GetDirection(), Time.deltaTime);
        }
    }
}