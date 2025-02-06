public abstract class TutorialStepController
{
    protected readonly TutorialState _tutorialState;
    protected readonly TutorialTextPanelController _textPanelController;

    protected TutorialStep _stepID;

    protected TutorialStepController(TutorialState tutorialState, TutorialTextPanelController textPanelController)
    {
        _tutorialState = tutorialState;
        _textPanelController = textPanelController;
        _tutorialState.OnStepStarted += OnStepStart;
    }

    private void OnStepStart(TutorialStep tutorialStep)
    {
        if (tutorialStep != _stepID) return;

        StartActions();
    }

    protected abstract void StartActions();
    protected abstract void FinishActions();
}