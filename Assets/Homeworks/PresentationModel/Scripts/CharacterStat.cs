using System;
using System.ComponentModel;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStat
    {
        public event Action<int> OnValueChanged; 

        [ReadOnly(true)]
        public string Name { get; private set; }

        [ReadOnly(true)]
        public int Value { get; private set; }

        [ContextMenu("ChangeValue")]
        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}