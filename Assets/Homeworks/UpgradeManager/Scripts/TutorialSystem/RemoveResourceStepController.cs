using Homeworks.UpgradeManager;

public sealed class RemoveResourceStepController : TutorialStepController
{
    private const string TutorialText =
        "Готово, теперь ты можешь собрать его кнопкой Remove в правой части экрана.";

    private readonly ButtonsViewController _buttonsViewController;
    private readonly StationHandler _stationHandler;

    public RemoveResourceStepController(TutorialState tutorialState,
        TutorialTextPanelController textPanelController,
        ButtonsViewController buttonsViewController, StationHandler stationHandler) : base(tutorialState,
        textPanelController)
    {
        _stepID = TutorialStep.REMOVE_RESOURCE;
        _stationHandler = stationHandler;
        _buttonsViewController = buttonsViewController;
    }

    protected override void StartActions()
    {
        _textPanelController.ShowText(TutorialText);
        _buttonsViewController.ShowRemoveButton();
        _buttonsViewController.RemoveButton.onClick.AddListener(FinishActions);
    }

    protected override void FinishActions()
    {
        _tutorialState.FinishStep();
        _buttonsViewController.RemoveButton.onClick.RemoveListener(FinishActions);
    }
}