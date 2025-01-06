using Homeworks.UpgradeManager;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class InputHandler : MonoBehaviour
{
    private UpgradeManager _upgradeManager;
    private MoneyStorage _moneyStorage;
    private UpgradePanelView _upgradePanel;

    private StationHandler _stationHandler;

    [SerializeField] private int _totalResourceValue;
    [SerializeField] private TextMeshProUGUI _moneyView;

    [Inject]
    public void Construct(UpgradeManager upgradeManager, UpgradePanelView upgradePanel, MoneyStorage moneyStorage,
        StationHandler stationHandler)
    {
        _upgradeManager = upgradeManager;
        _upgradePanel = upgradePanel;
        _stationHandler = stationHandler;

        _moneyStorage = moneyStorage;
    }

    private void Awake()
    {
        _upgradePanel.HidePanel();

        _moneyStorage.Money.Subscribe(delegate { _moneyView.text = _moneyStorage.Money.Value.ToString(); })
            .AddTo(this);
    }

    [ShowInInspector]
    public void OpenUpgradePanel()
    {
        _upgradePanel.ShowPanel(new UpgradePanelPresenter(_upgradeManager.GetAllUpgrades(), _moneyStorage));
    }

    [ShowInInspector]
    public void PutResources(int amount)
    {
        _stationHandler.PutResourcesToStation(amount, _totalResourceValue, out _totalResourceValue);
    }
    
    [ShowInInspector]
    public void RemoveResources()
    {
        _stationHandler.CollectAllResourcesFromOutArea();
    }
}