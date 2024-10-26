using UnityEngine;

namespace Lessons.Architecture.PM
{  
    [CreateAssetMenu(fileName = "Character", menuName = "Data/New Stat")]
    public sealed class CharacterStatData: ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _value;
        public string Name => _name;
        public int Value => _value;
    }
}