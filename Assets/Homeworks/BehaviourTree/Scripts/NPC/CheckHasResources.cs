namespace Homeworks.BehaviourTree
{
    public class CheckHasResources : Node
    {
        private readonly ResourcesContainer _resourcesContainer;

        public CheckHasResources(ResourcesContainer resourcesContainer)
        {
            _resourcesContainer = resourcesContainer;
        }

        public override NodeState Evaluate()
        {
            return _resourcesContainer.ResourceAmount > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}