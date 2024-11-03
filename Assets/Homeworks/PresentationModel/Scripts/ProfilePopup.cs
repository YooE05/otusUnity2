using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public class ProfilePopup : MonoBehaviour
    {
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private CharacterStatView _statViewPrefab;
        private readonly List<CharacterStatView> _statsViews = new List<CharacterStatView>();

        [SerializeField] private Button _closeButton;

        [Header("LevelUp")] 
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _sliderText;

        [SerializeField] private TextMeshProUGUI _levelText;

        [SerializeField] private Button _levelUpButton;
        [SerializeField] private Sprite _activeLevelUpSprite;
        [SerializeField] private Sprite _inactiveLevelUpSprite;
        
        [Header("UserInfo")] [SerializeField] private Image _characterIcon;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _nicknameText;

        private ProfilePresenter _presenter;

        public void Show(IPresenter args)
        {
            if (args is not ProfilePresenter)
            {
                throw new InvalidOperationException("Expected Profile Presenter");
            }

            gameObject.SetActive(true);

            _presenter = (ProfilePresenter) args;

            _closeButton.onClick.AddListener(Hide);

            _levelUpButton.onClick.AddListener(IncreaseAllStats);

            // _slider.value = _presenter.SliderValue;
            //  _sliderText.text = _presenter.SliderText;
            //  _levelText.text = _presenter.Level;
            //  CheckLevelUp(_presenter);

            _nicknameText.text = _presenter.Username;
            _descriptionText.text = _presenter.Description;
            _characterIcon.sprite = _presenter.Icon;

            _presenter.OnStatPresenterAdded += CreateStatPresenterView;

            foreach (var presenter in _presenter.StatPresenters)
            {
                CreateStatPresenterView(presenter);
            }
        }

        private void CreateStatPresenterView(CharacterStatPresenter statPresenter)
        {
            CharacterStatView stat = Instantiate(_statViewPrefab, _statsContainer);
            stat.Init(statPresenter);
            _statsViews.Add(stat);
        }

        private void IncreaseAllStats()
        {
            _presenter.InreaseStats();
        }

        private void CheckLevelUp(ProfilePresenter presenter)
        {
            _levelUpButton.image.sprite = presenter.CanLevelUp() ? _activeLevelUpSprite : _inactiveLevelUpSprite;
        }

        private void Hide()
        {
            _presenter.OnStatPresenterAdded -= CreateStatPresenterView;
            for (var i = _statsViews.Count - 1; i >= 0; i--)
            {
                CharacterStatView productView = _statsViews[i];
                Destroy(productView.gameObject);
            }

            _statsViews.Clear();

            gameObject.SetActive(false);
            _closeButton.onClick.RemoveListener(Hide);
            _levelUpButton.onClick.RemoveListener(IncreaseAllStats);
        }

        public void UpdateStats()
        {
            //_presenter?.UpdatePresentersData();

            for (var i = 0; i < _statsViews.Count; i++)
            {
                _statsViews[i].UpdateValue();
            }
        }
    }

    public sealed class ProfilePresenter : IPresenter
    {
        public event Action<CharacterStatPresenter> OnStatPresenterAdded;

        private readonly UserInfo _userData;

        private readonly CharacterStatsManager _statsManager;

        private readonly HashSet<CharacterStatPresenter> _statPresenters = new HashSet<CharacterStatPresenter>();
        // private readonly PlayerLevel _levelData;

        public ProfilePresenter(UserInfo userData, CharacterStatsManager statsManager)
        {
            _userData = userData;
            _statsManager = statsManager;

            var allStats = _statsManager.GetStats();
            for (int i = 0; i < allStats.Length; i++)
            {
                CreatePresenter(allStats[i]);
            }

            _statsManager.OnStatAdded += CreatePresenter;
        }

        public string Username => _userData.Name;
        public string Description => _userData.Description;
        public Sprite Icon => _userData.Icon;

        public HashSet<CharacterStatPresenter> StatPresenters => _statPresenters;

        private void CreatePresenter(CharacterStat characterStat)
        {
            var statPresenter = new CharacterStatPresenter(characterStat);
            if (_statPresenters.Add(statPresenter))
            {
                OnStatPresenterAdded?.Invoke(statPresenter);
            }
        }

        public bool CanLevelUp()
        {
            return true;
        }

        /*public void UpdatePresentersData()
        {
            var allStats = _statsManager.GetStats();

            foreach (var stat in allStats)
            {
                var statPresenter = _statPresenters.Find(s => s.Name == stat.Name);
                if (statPresenter != null)
                {
                    statPresenter.SetValue(stat.Value);
                }
                else
                {
                    CreatePresenter(stat);
                }
            }
        }*/

        public void InreaseStats()
        {
           _statsManager.IncreaseAllStats();
        }

        ~ProfilePresenter()
        {
            _statsManager.OnStatAdded -= CreatePresenter;
        }
    }
}