using UnityEngine;
using Zenject;

public class TutorialInstaller : MonoInstaller
{
    [SerializeField] private ButtonsViewController _buttonsViewController;
    [SerializeField] private TutorialTextPanelController _tutorialTextPanelController;
    
    public override void InstallBindings()
    {
        Container.Bind<TutorialState>().AsSingle().NonLazy();

        Container.Bind<AddResourceStepController>().AsSingle().NonLazy();
        Container.Bind<RemoveResourceStepController>().AsSingle().NonLazy();
        Container.Bind<UpgradeStepController>().AsSingle().NonLazy();

        Container.Bind<ButtonsViewController>().FromInstance(_buttonsViewController).AsSingle().NonLazy();
        Container.Bind<TutorialTextPanelController>().FromInstance(_tutorialTextPanelController).AsSingle().NonLazy();
    }
}