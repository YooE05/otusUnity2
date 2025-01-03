using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class GameRepository : IGameRepository
    {
        private Dictionary<string, string> _gameState = new Dictionary<string, string>();
        private const string SaveKey = "ajsdpwoqueqwpodju";

        public bool TryGetData<T>(out T data)
        {
            var key = typeof(T).ToString();

            if (!_gameState.TryGetValue(key, out string json))
            {
                data = default;
                return false;
            }

            data = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
        
        public void SetData<T>(T data)
        {
            var key = typeof(T).ToString();
            string json = JsonConvert.SerializeObject(data);
            _gameState[key] = json;
        }

        public void SaveState()
        {
            string jsonGameState = JsonConvert.SerializeObject(_gameState);
            PlayerPrefs.SetString(SaveKey, jsonGameState);
        }

        public void LoadState()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                string jsonGameState = PlayerPrefs.GetString(SaveKey);
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonGameState);
            }
        }
    }
}