using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class InputHelper : MonoBehaviour
    {
        [SerializeField] private int _additionalExp;
        [SerializeField] private ProfilePopup _profilePopup;

        [SerializeField] private string _statName;
        [SerializeField] private int _statValue;
        [SerializeField] private List<CharacterInfoData> _enableCharacters;

        private UserInfo _userInfo;
        private CharacterStatsManager _statsManager;
        private PlayerLevel _playerLevel;

        [Inject]
        public void Construct(UserInfo userInfo, CharacterStatsManager statsManager, PlayerLevel playerLevel)
        {
            _userInfo = userInfo;
            _statsManager = statsManager;
            _playerLevel = playerLevel;

            _profilePopup.Hide();
        }

        [ContextMenu("AddExperience")]
        public void AddExp()
        {
            _playerLevel.AddExperience(_additionalExp);
        }

        [ContextMenu("AddStatValue")]
        public void AddStatValue()
        {
            _statsManager.AddStatValue(_statName, _statValue);
        }

        [ContextMenu("ShowProfilePopup")]
        public void ShowProfilePopup()
        {
            _profilePopup.Hide();
            _profilePopup.Show(new ProfilePresenter(_userInfo, _statsManager, _playerLevel, _enableCharacters));
        }
    }
}