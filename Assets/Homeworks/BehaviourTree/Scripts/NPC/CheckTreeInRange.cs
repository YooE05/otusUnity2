using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class CheckTreeInRange : Node
    {
        private readonly Transform _transform;
        private readonly Animator _animator;
        private readonly float _getResourcesRange;
        private readonly NpcBT _npc;

        public CheckTreeInRange(Transform transform, Animator animator, float getResourcesRange, NpcBT npc)
        {
            _transform = transform;
            _animator = animator;
            _getResourcesRange = getResourcesRange;
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            if (!_npc.TargetResource)
            {
                _state = NodeState.FAILURE;
                Debug.Log("no target");
                return _state;
            }

            var treePosition =
                new Vector3(_npc.TargetResource.Transform.position.x, 0, _npc.TargetResource.Transform.position.z);
            if (Vector3.SqrMagnitude(_transform.position - treePosition) <= _getResourcesRange * _getResourcesRange)
            {
                _animator.SetBool("Attacking", true);
                _animator.SetBool("Walking", false);

                Debug.Log("target IN range");

                _state = NodeState.SUCCESS;
                return _state;
            }

            Debug.Log("target NO in range");
            _state = NodeState.FAILURE;
            return _state;
        }
    }
}