using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SampleGame
{
    public static class AddressablesHandler
    {
        public static async UniTask<T> LoadAsset<T>(string assetId)
        {
            return await Addressables.LoadAssetAsync<T>(assetId);
        }

        public static async UniTask<T> LoadAsset<T>(AssetReference assetReference)
        {
            return await assetReference.LoadAssetAsync<T>();
        }

        public static void UnloadAsset(GameObject cachedAsset)
        {
            if (cachedAsset != null)
                Addressables.ReleaseInstance(cachedAsset);
        }

        public static async UniTask<SceneInstance> LoadScene(string sceneId)
        {
            return await Addressables.LoadSceneAsync(sceneId);
        }

        public static async UniTask UnloadScene(SceneInstance scene)
        {
            await Addressables.UnloadSceneAsync(scene);
        }
    }
}