using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lessons.Architecture.PM
{
    public class UserInfoPresenter : IPresenter
    {
        private readonly UserInfo _userData;
        private readonly List<CharacterInfoData> _enableCharacters = new List<CharacterInfoData>();

        public UserInfoPresenter(UserInfo userData, List<CharacterInfoData> enableCharacters)
        {
            _userData = userData;
            _enableCharacters = enableCharacters;
        }

        public string Username => _userData.Name;
        public string Description => _userData.Description;
        public Sprite Icon => _userData.Icon;

        public void ChangeCharacter()
        {
            var rand = Random.Range(0, _enableCharacters.Count);
            var newCharacter = _enableCharacters[rand];

            _userData.ChangeCharacter(newCharacter);
        }
    }
}