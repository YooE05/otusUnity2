using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private Button _closePanelButton;

    private void Awake()
    {
        _closePanelButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _closePanelButton.onClick.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        _closePanelButton.onClick.RemoveAllListeners();
    }


    public void ShowText(string tutorialText)
    {
        _tutorialText.text = tutorialText;
    }

    public void ShowPanel()
    {
        _tutorialPanel.SetActive(true);
    }

    public void ShowCloseButton()
    {
        _closePanelButton.gameObject.SetActive(true);
    }

    private void HidePanel()
    {
        _tutorialPanel.SetActive(false);
    }
}