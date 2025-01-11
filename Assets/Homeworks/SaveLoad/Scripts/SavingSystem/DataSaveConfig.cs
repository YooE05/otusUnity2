using UnityEngine;

namespace Homeworks.SaveLoad
{
    [CreateAssetMenu(fileName = "DataSaveConfig", menuName = "Game/SavesConfig")]
    public sealed class DataSaveConfig : ScriptableObject
    {
        [HideInInspector] public string DirectoryPath;
        public string EncryptionKey = "OtusTheBestdlaajsdjkasgdjkajsdpwoqueqwpodju";
        public string DataFileName = "SaveFile";

        private void OnEnable()
        {
            DirectoryPath = Application.persistentDataPath;
        }
    }
}