using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly MenuLoader _menuLoader;
        private const string SceneKey = "GameScene";

        private AsyncOperationHandle<SceneInstance> _sceneHandle;

        public GameLoader(MenuLoader menuLoader)
        {
            _menuLoader = menuLoader;
            _menuLoader.OnBackToMenu += UnloadGame;
        }

        private void UnloadGame()
        {
            if (!_sceneHandle.Result.Scene.isLoaded)
            {
                return;
            }

            Addressables.UnloadSceneAsync(_sceneHandle, UnloadSceneOptions.None);
        }

        public void LoadGame()
        {
            InitSceneAsset();
        }

        private void InitSceneAsset()
        {
            if (_sceneHandle.IsValid() && _sceneHandle.IsDone)
            {
                return;
            }

            var activateOnLoad = false;

            _sceneHandle = Addressables.LoadSceneAsync(SceneKey, LoadSceneMode.Single, activateOnLoad);
            _sceneHandle.Completed += delegate { _sceneHandle.Result.ActivateAsync(); };
        }
    }
}