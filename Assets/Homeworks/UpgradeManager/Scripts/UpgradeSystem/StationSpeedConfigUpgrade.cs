using System;
using UnityEngine;

namespace Homeworks.UpgradeManager
{
    [Serializable]
    [CreateAssetMenu(fileName = "StationSpeedUpgradeConfig", menuName = "Config/New Station Speed Upgrade Config")]
    public class StationSpeedConfigUpgrade : UpgradeConfig
    {
        public StationTimeTable TimeTable;

        public override Upgrade Create()
        {
            return new StationSpeedUpgrade(this);
        }

        public float GetNewTime(int level)
        {
            return TimeTable.GetTimeToTransform(level);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            TimeTable.OnValidate(MaxLevel);
        }
    }
}