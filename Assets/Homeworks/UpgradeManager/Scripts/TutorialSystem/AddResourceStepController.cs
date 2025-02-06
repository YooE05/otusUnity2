
using Homeworks.UpgradeManager;

public sealed class AddResourceStepController : TutorialStepController
{
    private const string TutorialStartText =
        "Пройдём небольшой туториал. Чтобы добавить ресурсы на переработку, жми на кнопку Add в правой части экрана.";

    private const string TutorialWaitText =
        "Подождём, пока ресурс переработается.";


    private readonly ButtonsViewController _buttonsViewController;
    private readonly StationHandler _stationHandler;

    public AddResourceStepController(TutorialState tutorialState, TutorialTextPanelController textPanelController,
        ButtonsViewController buttonsViewController, StationHandler stationHandler) : base(tutorialState,
        textPanelController)
    {
        _stepID = TutorialStep.ADD_RESOURCE;
        _buttonsViewController = buttonsViewController;
        _stationHandler = stationHandler;
    }

    protected override void StartActions()
    {
        _textPanelController.ShowPanel();
        _textPanelController.ShowText(TutorialStartText);
        _buttonsViewController.ShowAddButton();
        _buttonsViewController.AddButton.onClick.AddListener(WaitResourceTransform);
    }

    private void WaitResourceTransform()
    {
        _tutorialState.FinishStep(false);
        _buttonsViewController.AddButton.onClick.RemoveListener(WaitResourceTransform);
        _stationHandler.OnResourceTransformed += FinishActions;
        _textPanelController.ShowText(TutorialWaitText);
    }

    protected override void FinishActions()
    {
        _stationHandler.OnResourceTransformed -= FinishActions;
        _tutorialState.RunNextStep();
    }
}