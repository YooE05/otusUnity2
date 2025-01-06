using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Homeworks.UpgradeManager
{
    public sealed class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private List<UpgradeConfig> _configs;

        private readonly List<Upgrade> _upgrades = new List<Upgrade>();
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Awake()
        {
            for (var i = 0; i < _configs.Count; i++)
            {
                var upgrade = _configs[i].Create();
                _container.Inject(upgrade);
                _upgrades.Add(upgrade);
            }
        }

        public Upgrade GetUpgrade(string upgradeId)
        {
            var upgrade = _upgrades.Find(u => u.Id == upgradeId);
            return upgrade;
        }

        public void DoUpgrade(string id)
        {
            var upgrade = _upgrades.Find(u => u.Id == id);
            upgrade.LevelUp();
        }

        public List<Upgrade> GetAllUpgrades()
        {
            return _upgrades;
        }
    }
}