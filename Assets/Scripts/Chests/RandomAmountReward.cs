using UnityEngine;

namespace Chests
{
    public abstract class RandomAmountReward : IReward
    {
        private readonly int _minAmount;
        private readonly int _maxAmount;

        protected int RewardValue;

        protected RandomAmountReward(int minAmount, int maxAmount)
        {
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }

        public virtual void Give()
        {
            RewardValue = Random.Range(_minAmount, _maxAmount);
        }
    }
}