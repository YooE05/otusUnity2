using System.Collections.Generic;
using Newtonsoft.Json;

namespace Homeworks.SaveLoad
{
    public sealed class GameRepository : IGameRepository
    {
        private Dictionary<string, string> _gameState = new Dictionary<string, string>();
        private readonly IDataStreamer _dataStreamer;

        public GameRepository(IDataStreamer dataStreamer)
        {
            _dataStreamer = dataStreamer;
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
            _dataStreamer.WriteData(_gameState);
        }

        public void LoadState()
        {
            _gameState = _dataStreamer.ReadData();
        }
    }
}