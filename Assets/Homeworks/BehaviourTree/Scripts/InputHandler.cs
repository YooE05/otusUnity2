using Homeworks.UpgradeManager;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Homeworks.BehaviourTree
{
    public class InputHandler : MonoBehaviour
    {
        private MoneyStorage _moneyStorage;
        private StationHandler _stationHandler;

        [SerializeField] private int _totalResourceValue;
        [SerializeField] private TextMeshProUGUI _moneyView;

        [Inject]
        public void Construct(MoneyStorage moneyStorage, StationHandler stationHandler)
        {
            _moneyStorage = moneyStorage;
            _stationHandler = stationHandler;
        }

        private void Awake()
        {
            _moneyStorage.Money.Subscribe(delegate { _moneyView.text = _moneyStorage.Money.Value.ToString(); })
                .AddTo(this);
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
}