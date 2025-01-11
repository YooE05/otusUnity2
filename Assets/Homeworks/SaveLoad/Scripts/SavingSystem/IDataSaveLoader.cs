using Zenject;

namespace Homeworks.SaveLoad
{
    public interface IDataSaveLoader
    {
        public void LoadData(IGameRepository gameRepository, DiContainer container);

        public void SaveData(IGameRepository gameRepository, DiContainer container);
    }
}