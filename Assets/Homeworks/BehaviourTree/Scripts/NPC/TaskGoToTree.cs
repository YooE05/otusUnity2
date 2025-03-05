using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskGoToTree : Node
    {
        private readonly Transform _transform;

        public TaskGoToTree(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            var tree = Blackboard.TreeTransform;
            var treePosition = new Vector3(tree.position.x, 0, tree.position.z);

            if (Vector3.Distance(_transform.position, treePosition) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, treePosition, NpcBT.Speed * Time.deltaTime);
                _transform.LookAt(treePosition);
            }

            _state = NodeState.RUNNING;
            return _state;
        }
    }
}