using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Homeworks.UpgradeManager
{
    public sealed class UpgradeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _upgradeName;
        [SerializeField] private TextMeshProUGUI _currentLevel;
        [SerializeField] private TextMeshProUGUI _value;

        [SerializeField] private Button _doUpgradeButton;
        [SerializeField] private TextMeshProUGUI _buttonText;

        private UpgradePresenter _presenter;

        public void SetUp(UpgradePresenter presenter)
        {
            _presenter = presenter;
            _presenter.OnMoneyChanged
                .Subscribe(delegate
                {
                    UpdateValues();
                    UpdateButtonState(_presenter.GetButtonState());
                })
                .AddTo(this);

            _doUpgradeButton.OnClickAsObservable()
                .Subscribe(delegate { _presenter.DoUpgrade(); })
                .AddTo(this);

            UpdateValues();
            UpdateButtonState(_presenter.GetButtonState());
        }

        private void UpdateValues()
        {
            _upgradeName.text = _presenter.UpgradeName;
            _currentLevel.text = $"Level {_presenter.Level}/{_presenter.MaxLevel}";
            _value.text = $"Value {_presenter.Value}";
        }

        private void UpdateButtonState(ButtonState buttonState)
        {
            switch (buttonState)
            {
                case ButtonState.CanBuy:
                    _buttonText.text = _presenter.Price;
                    _doUpgradeButton.image.color = Color.green;
                    _doUpgradeButton.enabled = true;
                    break;
                case ButtonState.CantBuy:
                    _buttonText.text = _presenter.Price;
                    _doUpgradeButton.image.color = Color.gray;
                    _doUpgradeButton.enabled = false;
                    break;
                case ButtonState.MaxValueReached:
                    _buttonText.text = "MAX";
                    _doUpgradeButton.image.color = Color.yellow;
                    _doUpgradeButton.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ButtonState), buttonState, null);
            }
        }
    }
}