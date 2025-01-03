using Zenject;

namespace Homeworks.SaveLoad
{
    public abstract class DataSaver<TData, TService> : IDataSaver
    {
        void IDataSaver.SaveData(IGameRepository gameRepository, DiContainer container)
        {
            var service = container.Resolve<TService>();
            TData data = ExtractData(service);
            gameRepository.SetData(data);
        }

        void IDataSaver.LoadData(IGameRepository gameRepository, DiContainer container)
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