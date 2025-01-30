using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class MoveController : IFixedTickable
    {
        private readonly ICharacter character;
        private readonly IMoveInput moveInput;

        private bool _canMove;

        public MoveController(ICharacter character, IMoveInput moveInput)
        {
            this.character = character;
            this.moveInput = moveInput;
            _canMove = true;
        }

        public void SetMoveAbility(bool canMove)
        {
            _canMove = canMove;
        }

        void IFixedTickable.FixedTick()
        {
            if(!_canMove) return;
            
            this.character.Move(this.moveInput.GetDirection(), Time.deltaTime);
        }
    }
}