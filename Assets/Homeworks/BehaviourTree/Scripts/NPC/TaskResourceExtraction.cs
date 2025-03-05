using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskResourceExtraction : Node
    {
        private readonly Animator _animator;
        private Resource _resource;
        private const float ExtractTime = 1f;
        private float _extractCounter = 0f;

        public TaskResourceExtraction(Animator animator)
        {
            _animator = animator;
        }

        public override NodeState Evaluate()
        {
            _resource = Blackboard.TreeResource;

            _extractCounter += Time.deltaTime;
            if (_extractCounter >= ExtractTime)
            {
                _resource.Get();
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Walking", true);
                NpcBT.ResourcesContainer.Add(1);
                _extractCounter = 0f;
            }

            _state = NodeState.RUNNING;
            return _state;
        }
    }
}