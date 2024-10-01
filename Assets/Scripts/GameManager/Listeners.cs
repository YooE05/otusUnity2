namespace ShootEmUp
{
    public class Listeners
    {
        public interface IGameListener
        {

        }

        public interface IInitListener : IGameListener
        {
            void OnInit();
        }

        public interface IStartListener : IGameListener
        {
            void OnStart();
        }
        
        public interface IFinishListener : IGameListener
        {
            void OnFinish();
        }
        
        public interface IPauseListener : IGameListener
        {
            void OnPause();
        }
        
        public interface IResumeListener : IGameListener
        {
            void OnResume();
        }
        
        public interface IUpdateListener : IGameListener
        {
            void OnUpdate(float deltaTime);
        }
        
        public interface IFixUpdaterListener : IGameListener
        {
            void OnFixedUpdate(float deltaTime);
        }
        
        public interface IPrestartUpdateListener : IGameListener
        {
            void OnPrestartUpdate(float deltaTime);
        }
    }
}