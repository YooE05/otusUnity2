using UnityEngine;

namespace Chests
{
    public class SoftCurrencyReward : RandomAmountReward
    {
        public SoftCurrencyReward(int minAmount, int maxAmount) : base(minAmount, maxAmount)
        {
        }

        public override void Give()
        {
            base.Give();
            Debug.Log($"Received {RewardValue} coins");
        }
    }
}