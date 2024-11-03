using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public class UserInfoView : MonoBehaviour
    {
        [SerializeField] private Image _characterIcon;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _nicknameText;

        [SerializeField] private Button _changeCharacterButton;

        private UserInfoPresenter _presenter;

        private void OnEnable()
        {
            _changeCharacterButton.onClick.AddListener(OnChangeCharacter);
        }

        private void OnDisable()
        {
            _changeCharacterButton.onClick.RemoveAllListeners();
        }

        public void Init(UserInfoPresenter userInfoPresenter)
        {
            _presenter = userInfoPresenter;

            _nicknameText.text = _presenter.Username;
            SetUpCharacterInfo();
        }

        private void OnChangeCharacter()
        {
            if (_presenter == null) return;

            _presenter.ChangeCharacter();
            SetUpCharacterInfo();
        }

        private void SetUpCharacterInfo()
        {
            _descriptionText.text = _presenter.Description;
            _characterIcon.sprite = _presenter.Icon;
        }
    }
}