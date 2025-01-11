using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class FileDataStreamer : IDataStreamer
    {
        private readonly DataSaveConfig _saveConfig;

        public FileDataStreamer(DataSaveConfig saveConfig)
        {
            _saveConfig = saveConfig;
        }

        public void WriteData(Dictionary<string, string> gameState)
        {
            var jsonGameState = JsonConvert.SerializeObject(gameState);
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

        public Dictionary<string, string> ReadData()
        {
            var fullPath = Path.Combine(_saveConfig.DirectoryPath, _saveConfig.DataFileName);
            if (!File.Exists(fullPath)) return new Dictionary<string, string>();

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
                Debug.Log("All data was loaded");
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonGameState);
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