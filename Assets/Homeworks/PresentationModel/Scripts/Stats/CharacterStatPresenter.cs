using System;

namespace Lessons.Architecture.PM
{
    public class CharacterStatPresenter : IPresenter
    {
        public Action<string> OnStatValueWasChanged;
        public string Name => _characterStat.Name;
        public string Value => _characterStat.Value.ToString();

        private readonly CharacterStat _characterStat;

        public CharacterStatPresenter(CharacterStat characterStat)
        {
            _characterStat = characterStat;
            _characterStat.OnValueChanged += ChangeStatViewValue;
        }

        private void ChangeStatViewValue(int newValue)
        {
            OnStatValueWasChanged?.Invoke(newValue.ToString());
        }

        public void SetValue(int newValue)
        {
            _characterStat.ChangeValue(newValue);
        }

        public void IncreaseValueByPercent(int percent)
        {
            var increasedValue = (int) (_characterStat.Value * (1 + percent / 100f));
            _characterStat.ChangeValue(increasedValue);
            OnStatValueWasChanged?.Invoke(increasedValue.ToString());
        }

        ~CharacterStatPresenter()
        {
            _characterStat.OnValueChanged -= ChangeStatViewValue;
        }
    }
}