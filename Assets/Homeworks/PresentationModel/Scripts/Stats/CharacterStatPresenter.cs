using System;

namespace Lessons.Architecture.PM
{
    public class CharacterStatPresenter: IPresenter
    {
        private string _name;
        private int _value;
        
        public string Name => _name;

        public string Value => _value.ToString(); 

        public CharacterStatPresenter(CharacterStat characterStat)
        {
            _name = characterStat.Name;
            _value = characterStat.Value;
        }

        public void SetValue(int newValue)
        {
            _value = newValue;
        }
    }
}