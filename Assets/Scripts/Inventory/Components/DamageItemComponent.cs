using System;

namespace Homework.Inventory
{
    [Serializable]
    public class DamageItemComponent : IItemComponent
    {
        public int Damage;

        public IItemComponent Clone()
        {
            return new DamageItemComponent {Damage = Damage};
        }
    }
}