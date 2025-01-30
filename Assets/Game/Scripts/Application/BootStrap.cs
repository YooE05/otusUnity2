using SampleGame;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class BootStrap : MonoBehaviour
{
    private UiAssetsContainer _uiAssetsContainer;

    [Inject]
    public void Construct(UiAssetsContainer uiAssetsContainer)
    {
        _uiAssetsContainer = uiAssetsContainer;
        _uiAssetsContainer.AllAssetsLoaded.Subscribe(delegate { TryLoadMenuScene(); });
        TryLoadMenuScene();
    }

    private void TryLoadMenuScene()
    {
        if (_uiAssetsContainer.AllAssetsLoaded.Value == true)
        {
            SceneManager.LoadScene("Game/Scenes/Menu");
        }
    }
}