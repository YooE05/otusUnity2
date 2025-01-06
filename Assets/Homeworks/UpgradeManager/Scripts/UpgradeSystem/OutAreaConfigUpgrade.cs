using System;
using UnityEngine;

namespace Homeworks.UpgradeManager
{
    [Serializable]
    [CreateAssetMenu(fileName = "OutAreaUpgradeConfig", menuName = "Config/New Out Area Upgrade Config")]
    public sealed class OutAreaConfigUpgrade : UpgradeConfig
    {
        public CapacityTable CapacityTable;

        public override Upgrade Create()
        {
            return new OutAreaCapacityUpgrade(this);
        }

        public int GetCapacity(int level)
        {
            return CapacityTable.GetCapacity(level);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            CapacityTable.OnValidate(MaxLevel);
        }
    }
}