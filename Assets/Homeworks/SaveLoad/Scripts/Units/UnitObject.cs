using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class UnitObject : MonoBehaviour
    {
        [SerializeField]
        public int HitPoints;

        [SerializeField]
        public int Speed;

        [SerializeField]
        public int Damage;
    }
}