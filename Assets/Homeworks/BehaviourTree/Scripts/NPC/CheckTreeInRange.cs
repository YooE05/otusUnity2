using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class CheckTreeInRange : Node
    {
        private readonly Transform _transform;
        private readonly Animator _animator;

        public CheckTreeInRange(Transform transform, Animator animator)
        {
            _transform = transform;
            _animator = animator;
        }

        public override NodeState Evaluate()
        {
            if (!Blackboard.IsTreeAvailable)
            {
                _state = NodeState.FAILURE;
                return _state;
            }

            var tree = Blackboard.TreeTransform;
            var treePosition = new Vector3(tree.position.x, 0, tree.position.z);

            if (Vector3.SqrMagnitude(_transform.position - treePosition) <=
                NpcBT.GetResourcesRange * NpcBT.GetResourcesRange)
            {
                _animator.SetBool("Attacking", true);
                _animator.SetBool("Walking", false);

                _state = NodeState.SUCCESS;
                return _state;
            }

            _state = NodeState.FAILURE;
            return _state;
        }
    }
}