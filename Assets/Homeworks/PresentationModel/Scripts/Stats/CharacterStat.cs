using System;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStat
    {
        public event Action<int> OnValueChanged;
        public string Name { get; private set; }
        public int Value { get; private set; }

        public CharacterStat(CharacterStatData data)
        {
            Name = data.Name;
            Value = data.Value;
        }
        
        public CharacterStat(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}