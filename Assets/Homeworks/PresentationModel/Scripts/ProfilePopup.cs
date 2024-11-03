using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public class ProfilePopup : MonoBehaviour
    {
        [Header("Main")] 
        [SerializeField] private Button _closeButton;
        
        [Header("Stats")] 
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private CharacterStatView _statViewPrefab;
        private readonly List<CharacterStatView> _statsViews = new List<CharacterStatView>();

        [Header("LevelUp")] 
        [SerializeField] private PlayerLevelView _levelView;

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

            _nicknameText.text = _presenter.Username;
            _descriptionText.text = _presenter.Description;
            _characterIcon.sprite = _presenter.Icon;

            _levelView.Init(_presenter.LevelPresenter);

            foreach (var presenter in _presenter.StatPresenters)
            {
                CreateStatView(presenter);
            }

            _presenter.OnStatPresenterAdded += CreateStatView;
        }

        private void CreateStatView(CharacterStatPresenter statPresenter)
        {
            CharacterStatView stat = Instantiate(_statViewPrefab, _statsContainer);
            stat.Init(statPresenter);
            _statsViews.Add(stat);
        }

        private void Hide()
        {
            _presenter.OnStatPresenterAdded -= CreateStatView;
            for (var i = _statsViews.Count - 1; i >= 0; i--)
            {
                CharacterStatView productView = _statsViews[i];
                Destroy(productView.gameObject);
            }

            _statsViews.Clear();

            _closeButton.onClick.RemoveListener(Hide);
            gameObject.SetActive(false);
        }
    }

    public sealed class ProfilePresenter : IPresenter
    {
        public event Action<CharacterStatPresenter> OnStatPresenterAdded;

        private readonly UserInfo _userData;
        private readonly CharacterStatsManager _statsManager;
        private readonly HashSet<CharacterStatPresenter> _statPresenters = new HashSet<CharacterStatPresenter>();
        private readonly PlayerLevel _levelInfo;
        private readonly PlayerLevelPresenter _levelPresenter;

        public ProfilePresenter(UserInfo userData, CharacterStatsManager statsManager, PlayerLevel levelInfo)
        {
            _userData = userData;

            _statsManager = statsManager;
            var allStats = _statsManager.GetStats();
            for (int i = 0; i < allStats.Length; i++)
            {
                CreatePresenter(allStats[i]);
            }

            _statsManager.OnStatAdded += CreatePresenter;

            _levelInfo = levelInfo;
            _levelPresenter = new PlayerLevelPresenter(_levelInfo);
            _levelPresenter.OnLevelUp += InreaseStats;
        }

        public string Username => _userData.Name;
        public string Description => _userData.Description;
        public Sprite Icon => _userData.Icon;
        public HashSet<CharacterStatPresenter> StatPresenters => _statPresenters;
        public PlayerLevelPresenter LevelPresenter => _levelPresenter;

        private void CreatePresenter(CharacterStat characterStat)
        {
            var statPresenter = new CharacterStatPresenter(characterStat);
            if (_statPresenters.Add(statPresenter))
            {
                OnStatPresenterAdded?.Invoke(statPresenter);
            }
        }

        private void InreaseStats()
        {
            _statsManager.IncreaseAllStats();
        }

        ~ProfilePresenter()
        {
            _statsManager.OnStatAdded -= CreatePresenter;
            _levelPresenter.OnLevelUp -= InreaseStats;
        }
    }
}