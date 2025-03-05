using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskGoToConverter : Node
    {
        private readonly Transform _transform;

        public TaskGoToConverter(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            var converter = Blackboard.ConverterStayPoint;
            var converterPosition = new Vector3(converter.position.x, 0, converter.position.z);

            if (Vector3.Distance(_transform.position, converterPosition) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, converterPosition, NpcBT.Speed * Time.deltaTime);
                _transform.LookAt(converterPosition);
                _state = NodeState.RUNNING;
            }
            else
            {
                _state = NodeState.SUCCESS;
            }

            return _state;
        }
    }
}