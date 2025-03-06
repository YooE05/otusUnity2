using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskGoToConverter : Node
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly Transform _converterStayPoint;

        public TaskGoToConverter(Transform transform, float speed, Transform converterStayPoint)
        {
            _transform = transform;
            _speed = speed;
            _converterStayPoint = converterStayPoint;
        }

        public override NodeState Evaluate()
        {
            var converterPosition = new Vector3(_converterStayPoint.position.x, 0, _converterStayPoint.position.z);

            if (Vector3.Distance(_transform.position, converterPosition) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, converterPosition, _speed * Time.deltaTime);
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