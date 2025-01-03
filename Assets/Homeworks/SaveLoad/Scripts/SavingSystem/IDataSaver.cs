using Zenject;

namespace Homeworks.SaveLoad
{
    public interface IDataSaver
    {
        public void LoadData(IGameRepository gameRepository, DiContainer container);

        public void SaveData(IGameRepository gameRepository, DiContainer container);
    }
}