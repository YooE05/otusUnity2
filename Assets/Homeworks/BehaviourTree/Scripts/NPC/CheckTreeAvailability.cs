using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class CheckTreeAvailability : Node
    {
        private readonly Animator _animator;

        public CheckTreeAvailability(Animator animator)
        {
            _animator = animator;
        }

        public override NodeState Evaluate()
        {
            NodeState state;

            if (Blackboard.IsTreeAvailable)
            {
                _animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.FAILURE;
            }

            return state;
        }
    }
}