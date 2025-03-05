using System;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskPatrol : Node
    {
        private readonly Transform _transform;
        private readonly Animator _animator;
        private readonly Transform[] _waypoints;

        private int _currentWaypointIndex = 0;

        private const float WaitTime = 1f;
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public TaskPatrol(Transform transform, Transform[] waypoints, Animator animator)
        {
            _transform = transform;
            _animator = animator;
            _waypoints = waypoints;
        }

        public override NodeState Evaluate()
        {
            var wp = _waypoints[_currentWaypointIndex];
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= WaitTime)
                {
                    _waiting = false;
                    _animator.SetBool("Walking", true);
                }
            }
            else
            {
                if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
                {
                    _transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                    _animator.SetBool("Walking", false);
                }
                else
                {
                    _transform.position =
                        Vector3.MoveTowards(_transform.position, wp.position, NpcBT.Speed * Time.deltaTime);
                    _transform.LookAt(wp.position);
                    _animator.SetBool("Walking", true);
                }
            }

            _state = NodeState.RUNNING;
            return _state;
        }
    }
}