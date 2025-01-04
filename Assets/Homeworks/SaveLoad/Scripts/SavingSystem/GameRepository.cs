using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class GameRepository : IGameRepository
    {
        private Dictionary<string, string> _gameState = new Dictionary<string, string>();
        private readonly DataSaveConfig _saveConfig;

        public GameRepository(DataSaveConfig saveConfig)
        {
            _saveConfig = saveConfig;
        }

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
            string json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            _gameState[key] = json;
        }

        public void SaveState()
        {
            var jsonGameState = JsonConvert.SerializeObject(_gameState);
            var stringGameState = EncryptDecrypt(jsonGameState);
            var fullPath = Path.Combine(_saveConfig.DirectoryPath, _saveConfig.DataFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(stringGameState);
                    }
                }

                Debug.Log("All data was saved");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while saving data - {e}");
                throw;
            }
        }

        public void LoadState()
        {
            var fullPath = Path.Combine(_saveConfig.DirectoryPath, _saveConfig.DataFileName);
            if (!File.Exists(fullPath)) return;

            try
            {
                string stringGameState;
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        stringGameState = reader.ReadToEnd();
                    }
                }

                string jsonGameState = EncryptDecrypt(stringGameState);
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonGameState);
                Debug.Log("All data was loaded");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while loading data - {e}");
                throw;
            }
        }

        private string EncryptDecrypt(string data)
        {
            var modifiedData = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char) (data[i] ^ _saveConfig.EncryptionKey[i % _saveConfig.EncryptionKey.Length]);
            }

            return modifiedData;
        }
    }
}