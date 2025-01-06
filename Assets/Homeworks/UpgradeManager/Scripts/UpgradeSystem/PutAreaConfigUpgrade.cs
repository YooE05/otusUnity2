using System;
using UnityEngine;

namespace Homeworks.UpgradeManager
{
    [Serializable]
    [CreateAssetMenu(fileName = "PutAreaUpgradeConfig", menuName = "Config/New Put Area Upgrade Config")]
    public sealed class PutAreaConfigUpgrade : UpgradeConfig
    {
        public CapacityTable CapacityTable;

        public override Upgrade Create()
        {
            return new PutAreaCapacityUpgrade(this);
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