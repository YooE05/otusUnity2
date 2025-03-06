using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskResourceExtraction : Node
    {
        private const float ExtractTime = 1f;
        private float _extractCounter = 0f;

        private readonly Animator _animator;
        private readonly ResourcesContainer _resourcesContainer;
        private readonly NpcBT _npc;

        public TaskResourceExtraction(Animator animator, ResourcesContainer resourcesContainer, NpcBT npc)
        {
            _animator = animator;
            _resourcesContainer = resourcesContainer;
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            _extractCounter += Time.deltaTime;
            if (_extractCounter >= ExtractTime)
            {
                _npc.TargetResource.Get();
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Walking", true);
                _resourcesContainer.Add(1);
                _extractCounter = 0f;

                Debug.Log("target cleared");
                _npc.TargetResource = null;
            }

            _state = NodeState.RUNNING;
            return _state;
        }
    }
}