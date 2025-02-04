using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SampleGame
{
    public static class AddressablesHandler
    {
        public static async UniTask<GameObject> InstantiateAsset(AssetReference assetReference, Transform parent)
        {
            return await Addressables.InstantiateAsync(assetReference, parent);
        }

        public static async UniTask<T> LoadAsset<T>(AssetReference assetReference)
        {
            return await assetReference.LoadAssetAsync<T>();
        }

        public static async UniTask<AsyncOperationHandle> LoadAssetHandle<T>(AssetReference assetReference)
        {
            var handle = assetReference.LoadAssetAsync<GameObject>();
            await handle;
            return handle;
        }

        public static void UnloadAsset(GameObject cachedAsset)
        {
            if (cachedAsset != null)
                Addressables.Release(cachedAsset);
        }

        public static void UnloadAssetHandler(AsyncOperationHandle assetHandle)
        {
            Addressables.Release(assetHandle);
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