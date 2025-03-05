using Homeworks.UpgradeManager;
using UnityEngine;

namespace Homeworks.BehaviourTree
{
    public class TaskPutResources : Node
    {
        private readonly Animator _animator;
        private Transform _lastTarget;
        private StationHandler _converter;
        private const float PutTime = 1f;
        private float _putCounter = 0f;

        public TaskPutResources(Animator animator)
        {
            _animator = animator;
        }

        public override NodeState Evaluate()
        {
            _animator.SetBool("Walking", false);
            _animator.SetBool("Attacking", true);

            _converter = Blackboard.Converter;

            _putCounter += Time.deltaTime;
            if (_putCounter >= PutTime)
            {
                _converter.PutResourcesToStation(NpcBT.ResourcesContainer.ResourceAmount,
                    NpcBT.ResourcesContainer.ResourceAmount, out var left);
                NpcBT.ResourcesContainer.Remove(NpcBT.ResourcesContainer.ResourceAmount - left);

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