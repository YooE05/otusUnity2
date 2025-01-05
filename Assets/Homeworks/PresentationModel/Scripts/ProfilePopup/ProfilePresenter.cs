using System;
using System.Collections.Generic;

namespace Lessons.Architecture.PM
{
    public sealed class ProfilePresenter : IPresenter
    {
        public event Action<CharacterStatPresenter> OnStatPresenterAdded;

        private readonly UserInfo _userData;
        private readonly CharacterStatsManager _statsManager;
        private readonly PlayerLevel _levelInfo;

        private readonly HashSet<CharacterStatPresenter> _statPresenters = new HashSet<CharacterStatPresenter>();
        private readonly PlayerLevelPresenter _levelPresenter;
        private readonly UserInfoPresenter _userInfoPresenter;

        public ProfilePresenter(UserInfo userData, CharacterStatsManager statsManager, PlayerLevel levelInfo,
            List<CharacterInfoData> enableCharacters)
        {
            _userData = userData;
            _userInfoPresenter = new UserInfoPresenter(_userData, enableCharacters);

            _statsManager = statsManager;
            var allStats = _statsManager.GetStats();
            for (int i = 0; i < allStats.Length; i++)
            {
                CreatePresenter(allStats[i]);
            }

            _statsManager.OnStatAdded += CreatePresenter;

            _levelInfo = levelInfo;
            _levelPresenter = new PlayerLevelPresenter(_levelInfo);
            _levelPresenter.OnLevelUp += IncreaseStats;
        }

        public HashSet<CharacterStatPresenter> StatPresenters => _statPresenters;
        public PlayerLevelPresenter LevelPresenter => _levelPresenter;
        public UserInfoPresenter UserInfoPresenter => _userInfoPresenter;

        private void CreatePresenter(CharacterStat characterStat)
        {
            var statPresenter = new CharacterStatPresenter(characterStat);
            if (_statPresenters.Add(statPresenter))
            {
                OnStatPresenterAdded?.Invoke(statPresenter);
            }
        }

        private void IncreaseStats()
        {
            _statsManager.IncreaseAllStats();
        }

        ~ProfilePresenter()
        {
            _statsManager.OnStatAdded -= CreatePresenter;
            _levelPresenter.OnLevelUp -= IncreaseStats;
        }
    }
}