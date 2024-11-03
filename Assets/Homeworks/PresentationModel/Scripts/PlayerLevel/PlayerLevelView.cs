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

        private void OnEnable()
        {
            _levelUpButton.onClick.AddListener(OnLevelUp);
        }

        private void OnDisable()
        {
            _levelUpButton.onClick.RemoveAllListeners();
        }

        public void Init(PlayerLevelPresenter levelPresenter)
        {
            _presenter = levelPresenter;

            SetupValues();

            _presenter.OnExperienceCountChanged += SetupValues;
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
            if(_presenter==null) return;
            
            _presenter.LevelUp();
            SetupValues();
        }

        ~PlayerLevelView()
        {
            if (_presenter != null)
            {
                _presenter.OnExperienceCountChanged -= SetupValues;
            }
        }
    }
}