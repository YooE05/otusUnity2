using Zenject;

namespace Homeworks.SaveLoad
{
    public abstract class DataSaveLoader<TData, TService> : IDataSaveLoader
    {
        void IDataSaveLoader.SaveData(IGameRepository gameRepository, DiContainer container)
        {
            var service = container.Resolve<TService>();
            TData data = ExtractData(service);
            gameRepository.SetData(data);
        }

        void IDataSaveLoader.LoadData(IGameRepository gameRepository, DiContainer container)
        {
            var service = container.Resolve<TService>();
            
            if (!gameRepository.TryGetData(out TData data))
            {
                SetupDefaultData(service);
                return;
            }

            SetupData(service, data);
        }

        protected virtual void SetupDefaultData(TService service) { }
        protected abstract TData ExtractData(TService service);
        protected abstract void SetupData(TService service, TData data);
    }
}