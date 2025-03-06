using System.Collections.Generic;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskGoToTree : Node
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly ResourcesSpawner _resourcesSpawner;
        private readonly NpcBT _npc;

        public TaskGoToTree(Transform transform, float speed, ResourcesSpawner resourcesSpawner,
            NpcBT npc)
        {
            _transform = transform;
            _speed = speed;
            _resourcesSpawner = resourcesSpawner;
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            if (!_npc.TargetResource)
            {
                var trees = _resourcesSpawner.GetAvailableResources();
                _npc.TargetResource = FindClosestTree(trees);
            }

            var needTreePosition =
                new Vector3(_npc.TargetResource.Transform.position.x, 0, _npc.TargetResource.Transform.position.z);
            if (Vector3.Distance(_transform.position, needTreePosition) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, needTreePosition, _speed * Time.deltaTime);
                _transform.LookAt(needTreePosition);
            }

            _state = NodeState.RUNNING;
            return _state;
        }

        private Resource FindClosestTree(List<Resource> trees)
        {
            var needTreePosition = new Vector3(trees[0].Transform.position.x, 0, trees[0].Transform.position.z);
            var sqrMagnitude = Vector3.SqrMagnitude(needTreePosition - _transform.position);
            var needIndex = 0;

            for (var i = 0; i < trees.Count - 1; i++)
            {
                var position = new Vector3(trees[i].Transform.position.x, 0, trees[i].Transform.position.z);
                var tempSqrMagnitude = Vector3.SqrMagnitude(position - _transform.position);

                if (tempSqrMagnitude < sqrMagnitude)
                {
                    sqrMagnitude = tempSqrMagnitude;
                    needIndex = i;
                }
            }

            return trees[needIndex];
        }
    }
}