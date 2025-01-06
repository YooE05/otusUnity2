using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Homeworks.UpgradeManager
{
    public sealed class UpgradePanelView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _scrollViewContent;
        [SerializeField] private GameObject _upgradePanel;

        [SerializeField] private UpgradeView _upgradeViewPrefab;

        private readonly List<UpgradeView> _upgradeViews = new List<UpgradeView>();

        public void ShowPanel(UpgradePanelPresenter presenter)
        {
            //_closeButton.onClick.AddListener(ClosePanel);
            _closeButton.OnClickAsObservable().Subscribe(delegate { HidePanel(); }).AddTo(this);
            _upgradePanel.SetActive(true);

            for (int i = 0; i < presenter.UpgradePresenters.Count; i++)
            {
                var upgradeView = Instantiate(_upgradeViewPrefab, _scrollViewContent);
                upgradeView.SetUp(presenter.UpgradePresenters[i]);
                _upgradeViews.Add(upgradeView);
            }
        }

        public void HidePanel()
        {
            for (int i = _upgradeViews.Count - 1; i >= 0; i--)
            {
                Destroy(_upgradeViews[i].gameObject);
            }

            _upgradeViews.Clear();

            // _closeButton.onClick.RemoveAllListeners();
            _upgradePanel.SetActive(false);
        }
    }

    public enum ButtonState
    {
        CanBuy = 0,
        CantBuy = 1,
        MaxValueReached = 2
    }
}