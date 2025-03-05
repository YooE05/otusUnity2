using UnityEngine;

namespace Chests
{
    public class ResourceReward : RandomAmountReward
    {
        private readonly string _resourceName;

        public ResourceReward(string resourceName, int minAmount, int maxAmount) : base(minAmount, maxAmount)
        {
            _resourceName = resourceName;
        }

        public override void Give()
        {
            base.Give();
            Debug.Log($"Received {RewardValue} {_resourceName}");
        }
    }
}