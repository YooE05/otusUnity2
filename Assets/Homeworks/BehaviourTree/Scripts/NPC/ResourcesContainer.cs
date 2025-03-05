namespace Homeworks.BehaviourTree
{
    public class ResourcesContainer
    {
        public int ResourceAmount { get; private set; } = 0;

        public void Remove(int removeAmount)
        {
            ResourceAmount -= removeAmount;
        }

        public void Add(int addAmount)
        {
            ResourceAmount += addAmount;
        }
    }
}