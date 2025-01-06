using System;
using UnityEngine;

namespace Homeworks.UpgradeManager
{
    [Serializable]
    [CreateAssetMenu(fileName = "UpgradeConfig", menuName = "Config/New Upgrade Config")]
    public abstract class UpgradeConfig: ScriptableObject
    {
        public string Id;
        public int MaxLevel;
        public UpgradePriceTable PriceTable;

        public abstract Upgrade Create();

        public int GetNextPrice(int level)
        {
            return PriceTable.GetPrice(level);
        }

        protected virtual void OnValidate()
        {
            PriceTable.OnValidate(MaxLevel);
        }
    }
}