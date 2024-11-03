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

        private UserInfo _userInfo;
        private CharacterStatsManager _statsManager;
        
        [Inject]
        public void Construct( UserInfo userInfo, CharacterStatsManager statsManager)
        {
            _userInfo = userInfo;
            _statsManager = statsManager;
        }

        public void AddExp()
        {
        }
        
        [ContextMenu("AddStatValue")]
        public void AddStatValue()
        {
            _statsManager.AddStatValue(_statName,_statValue);
           // _profilePopup.UpdateStats();
        }
       
        [ContextMenu("ShowProfilePopup")]
        public void ShowProfilePopup()
        {
            _profilePopup.Show(new ProfilePresenter(_userInfo, _statsManager));
        }
    }
}