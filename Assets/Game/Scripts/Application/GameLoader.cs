using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private SceneInstance _gameScene;

        public async UniTask UnloadGame()
        {
            await AddressablesHandler.UnloadScene(_gameScene);
        }

        public async void LoadGame()
        {
            _gameScene = await AddressablesHandler.LoadScene("GameScene");
        }
    }
}