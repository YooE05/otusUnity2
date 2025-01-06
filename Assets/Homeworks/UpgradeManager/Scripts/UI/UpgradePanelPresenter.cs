using System.Collections.Generic;

namespace Homeworks.UpgradeManager
{
    public sealed class UpgradePanelPresenter
    {
        public readonly List<UpgradePresenter> UpgradePresenters = new List<UpgradePresenter>();

        public UpgradePanelPresenter(List<Upgrade> upgrades, MoneyStorage moneyStorage)
        {
            for (int i = 0; i < upgrades.Count; i++)
            {
                var upgradePresenter = new UpgradePresenter(upgrades[i], moneyStorage);
                UpgradePresenters.Add(upgradePresenter);
            }
        }
    }
}