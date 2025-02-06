using System;

public class TutorialState
{
    public event Action<TutorialStep> OnStepStarted;
    public event Action<TutorialStep> OnStepFinished;

    public TutorialStep CurrentStep;

    public void RunStep(TutorialStep step)
    {
        CurrentStep = step;
        OnStepStarted?.Invoke(CurrentStep);
    }

    public void RunNextStep()
    {
        if (CurrentStep + 1 != TutorialStep.END)
        {
            CurrentStep++;
            OnStepStarted?.Invoke(CurrentStep);
        }
    }

    public void FinishStep(bool needRunNext = true)
    {
        OnStepFinished?.Invoke(CurrentStep);
        if (needRunNext)
            RunNextStep();
    }
}