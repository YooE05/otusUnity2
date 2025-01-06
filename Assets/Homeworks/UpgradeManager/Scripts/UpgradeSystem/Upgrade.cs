using UnityEngine;

namespace Homeworks.UpgradeManager
{
    public abstract class Upgrade
    {
        public int MaxLevel => _config.MaxLevel;
        public int Level => _level;
        public string Id => _config.Id;
        public int NextPrice => _config.GetNextPrice(_level + 1);

        private int _level;
        private readonly UpgradeConfig _config;

        public bool IsMaxLevel => _level == MaxLevel;

        protected Upgrade(UpgradeConfig config)
        {
            _config = config;
            _level = 1;
        }

        public void LevelUp()
        {
            _level = Mathf.Clamp(_level + 1, _level, MaxLevel);

            OnUpgrade();
        }

        public abstract string GetCurrentValue();

        protected abstract void OnUpgrade();
    }
}