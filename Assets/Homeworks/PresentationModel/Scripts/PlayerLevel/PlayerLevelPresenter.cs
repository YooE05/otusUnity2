using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
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