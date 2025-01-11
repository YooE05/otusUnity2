using System.Collections.Generic;

namespace Homeworks.SaveLoad
{
    public interface IDataStreamer
    {
        public void WriteData(Dictionary<string, string> gameState);
        public Dictionary<string, string> ReadData();
    }
}