using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class CharacterStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _value;

        private CharacterStatPresenter _presenter;

        public void Init(CharacterStatPresenter presenter)
        {
            _presenter = presenter;
            _presenter.OnStatValueWasChanged += SetValue;

            SetName(_presenter.Name);
            SetValue(_presenter.Value);
        }

        private void SetName(string newName)
        {
            _name.text = newName;
        }

        private void SetValue(string newValue)
        {
            _value.text = newValue;
        }

        ~CharacterStatView()
        {
            if (_presenter?.OnStatValueWasChanged != null)
                _presenter.OnStatValueWasChanged -= SetValue;
        }
    }
}