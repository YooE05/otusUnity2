using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AssetReference[] _uiAssetReferences;

        public override void InstallBindings()
        {
            Container.Bind<UiAssetsContainer>().AsSingle().WithArguments(_uiAssetReferences).NonLazy();
            Container.Bind<ApplicationExiter>().AsSingle().NonLazy();
            Container.Bind<GameLoader>().AsSingle().NonLazy();
            Container.Bind<MenuLoader>().AsSingle().NonLazy();
        }
    }
}