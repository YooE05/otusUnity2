using UnityEngine;
using UnityEngine.UI;

public class ButtonsViewController : MonoBehaviour
{
    public Button AddButton;
    public Button RemoveButton;
    public Button UpgradesButton;

    private void Awake()
    {
        AddButton.gameObject.SetActive(false);
        UpgradesButton.gameObject.SetActive(false);
        RemoveButton.gameObject.SetActive(false);
    }

    public void ShowAddButton()
    {
        AddButton.gameObject.SetActive(true);
    }

    public void ShowRemoveButton()
    {
        RemoveButton.gameObject.SetActive(true);
    }

    public void ShowUpgradesButton()
    {
        UpgradesButton.gameObject.SetActive(true);
    }
}