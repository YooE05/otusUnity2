using Homeworks.UpgradeManager;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskPutResources : Node
    {
        private Transform _lastTarget;
        private const float PutTime = 1f;
        private float _putCounter = 0f;

        private readonly Animator _animator;
        private readonly StationHandler _converter;
        private readonly ResourcesContainer _resourcesContainer;

        public TaskPutResources(Animator animator, StationHandler converter, ResourcesContainer resourcesContainer)
        {
            _animator = animator;
            _converter = converter;
            _resourcesContainer = resourcesContainer;
        }

        public override NodeState Evaluate()
        {
            _animator.SetBool("Walking", false);
            _animator.SetBool("Attacking", true);

            _putCounter += Time.deltaTime;
            if (_putCounter >= PutTime)
            {
                _converter.PutResourcesToStation(_resourcesContainer.ResourceAmount,
                    _resourcesContainer.ResourceAmount, out var left);
                _resourcesContainer.Remove(_resourcesContainer.ResourceAmount - left);

                _animator.SetBool("Attacking", false);
                _animator.SetBool("Walking", true);
                _putCounter = 0f;

                _state = NodeState.SUCCESS;
                return _state;
            }

            _state = NodeState.RUNNING;
            return _state;
        }
    }
}