using Homeworks.UpgradeManager;

public sealed class UpgradeStepController : TutorialStepController
{
    private const string TutorialDoUpgradeText =
        "Ресурсы переработаны и собраны! Но ты можешь делать это быстрее, улучшив станцию во вкладке Upgrades, попробуй";

    private const string TutorialEndText =
        "Отлично, теперь станция работает быстрее. Я обучил тебя всему, что знаю, хорошей игры!";

    private readonly ButtonsViewController _buttonsViewController;
    private readonly StationHandler _stationHandler;

    public UpgradeStepController(TutorialState tutorialState,
        TutorialTextPanelController textPanelController,
        ButtonsViewController buttonsViewController, StationHandler stationHandler) : base(tutorialState,
        textPanelController)
    {
        _stepID = TutorialStep.DO_UPGRAGE;
        _stationHandler = stationHandler;
        _buttonsViewController = buttonsViewController;
    }

    protected override void StartActions()
    {
        _textPanelController.ShowText(TutorialDoUpgradeText);
        _buttonsViewController.ShowUpgradesButton();
        _stationHandler.OnNewTransformSpeedSet += FinishActions;
    }

    protected override void FinishActions()
    {
        _stationHandler.OnNewTransformSpeedSet -= FinishActions;

        _textPanelController.ShowText(TutorialEndText);
        _textPanelController.ShowCloseButton();
        _tutorialState.FinishStep();
    }
}