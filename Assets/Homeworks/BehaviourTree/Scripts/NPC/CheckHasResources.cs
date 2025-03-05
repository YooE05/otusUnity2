namespace Homeworks.BehaviourTree
{
    public class CheckHasResources : Node
    {
        public override NodeState Evaluate()
        {
            return NpcBT.ResourcesContainer.ResourceAmount > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}