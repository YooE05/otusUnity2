using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _sliderText;
        [SerializeField] private TextMeshProUGUI _levelText;

        [SerializeField] private Button _levelUpButton;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        private PlayerLevelPresenter _presenter;

        public void Init(PlayerLevelPresenter levelPresenter)
        {
            _presenter = levelPresenter;

            SetupValues();

            _presenter.OnExperienceCountChanged += SetupValues;
            _levelUpButton.onClick.AddListener(OnLevelUp);
        }

        private void SetupValues()
        {
            UpdateSlider();
            CheckButtonState();
            _levelText.text = _presenter.Level;
        }

        private void UpdateSlider()
        {
            _slider.value = _presenter.SliderQuotient;
            _sliderText.text = _presenter.SliderText;
        }

        private void CheckButtonState()
        {
            if (_presenter.CanLevelUp)
            {
                _levelUpButton.interactable = true;
                _levelUpButton.image.sprite = _activeSprite;
            }
            else
            {
                _levelUpButton.interactable = false;
                _levelUpButton.image.sprite = _inactiveSprite;
            }
        }

        private void OnLevelUp()
        {
            _presenter.LevelUp();
            SetupValues();
        }

        ~PlayerLevelView()
        {
            if (_presenter != null)
            {
                _presenter.OnExperienceCountChanged -= SetupValues;
            }

            _levelUpButton.onClick.RemoveAllListeners();
        }
    }

    public class PlayerLevelPresenter : IPresenter
    {
        public event Action OnExperienceCountChanged;
        public event Action OnLevelUp;

        private readonly PlayerLevel _playerLevelInfo;

        public PlayerLevelPresenter(PlayerLevel playerLevelInfo)
        {
            _playerLevelInfo = playerLevelInfo;
            _playerLevelInfo.OnExperienceChanged += UpdateView;
        }

        public float SliderQuotient =>
            Mathf.Clamp((float) _playerLevelInfo.CurrentExperience / _playerLevelInfo.RequiredExperience, 0f, 1f);

        public string SliderText =>
            $"XP: {_playerLevelInfo.CurrentExperience} / {_playerLevelInfo.RequiredExperience} ";

        public string Level => _playerLevelInfo.CurrentLevel.ToString();
        public bool CanLevelUp => _playerLevelInfo.CanLevelUp();

        private void UpdateView(int _)
        {
            OnExperienceCountChanged?.Invoke();
        }

        public void LevelUp()
        {
            _playerLevelInfo.LevelUp();
            OnLevelUp?.Invoke();
        }

        ~PlayerLevelPresenter()
        {
            _playerLevelInfo.OnExperienceChanged -= UpdateView;
        }
    }
}