using System;
using Homeworks.UpgradeManager;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class InputHandler : MonoBehaviour
{
    private UpgradeManager _upgradeManager;
    private MoneyStorage _moneyStorage;
    private UpgradePanelView _upgradePanel;

    private StationHandler _stationHandler;

    [SerializeField] private int _totalResourceValue;

    private void Awake()
    {
        _upgradePanel.HidePanel();
    }

    [Inject]
    public void Construct(UpgradeManager upgradeManager, UpgradePanelView upgradePanel, MoneyStorage moneyStorage,
        StationHandler stationHandler)
    {
        _upgradeManager = upgradeManager;
        _upgradePanel = upgradePanel;
        _moneyStorage = moneyStorage;

        _stationHandler = stationHandler;
    }

    [ShowInInspector]
    public void PutResources(int amount)
    {
        _stationHandler.PutResourcesToStation(amount, _totalResourceValue, out _totalResourceValue);
    }

    [ShowInInspector]
    public void OpenUpgradePanel()
    {
        _upgradePanel.ShowPanel(new UpgradePanelPresenter(_upgradeManager.GetAllUpgrades(), _moneyStorage));
    }
}