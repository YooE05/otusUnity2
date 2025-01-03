namespace Homeworks.SaveLoad
{
    public interface IGameRepository
    {
        bool TryGetData<T>(out T value);
        void SetData<T>(T value);
        void SaveState();
        void LoadState();
    }
}