using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshPro _levelText;

        [SerializeField] private Button _levelUpButton;
        [SerializeField] private Sprite _activeLevelUpSprite;
        [SerializeField] private Sprite _inactiveLevelUpSprite;

        private PlayerLevelPresenter _levelPresenter;

        /*[Inject]
        public void Construct(PlayerLevelPresenter levelPresenter)
        {
            _levelPresenter = levelPresenter;
        }*/

        public void UpdateSlider(float sliderValue)
        {
            _slider.value = sliderValue;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            throw new System.NotImplementedException();
        }
    }

    public class PlayerLevelPresenter
    {
    }
}