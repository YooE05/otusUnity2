using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class CheckTreeAvailability : Node
    {
        private readonly Animator _animator;
        private readonly ResourcesSpawner _resourcesSpawner;

        public CheckTreeAvailability(Animator animator, ResourcesSpawner resourcesSpawner)
        {
            _animator = animator;
            _resourcesSpawner = resourcesSpawner;
        }

        public override NodeState Evaluate()
        {
            NodeState state;

            if (_resourcesSpawner.IsResourcesAvailable)
            {
                _animator.SetBool("Attacking", false);
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