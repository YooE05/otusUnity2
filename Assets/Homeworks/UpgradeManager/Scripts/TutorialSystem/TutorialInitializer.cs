using UnityEngine;
using Zenject;

public sealed class TutorialInitializer : MonoBehaviour
{
    private TutorialState _tutorialState;

    [Inject]
    public void Construct(TutorialState tutorialState)
    {
        _tutorialState = tutorialState;
    }

    private void Start()
    {
        _tutorialState.OnStepFinished += LogFinishedStep;
        _tutorialState.RunStep(TutorialStep.ADD_RESOURCE);
    }

    private void LogFinishedStep(TutorialStep obj)
    {
        Debug.Log($"Step {obj} is finished");
    }
}