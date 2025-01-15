using System;

namespace Homework.Inventory
{
    [Serializable]
    public class ManaItemComponent : IItemComponent
    {
        public int Mana;

        public IItemComponent Clone()
        {
            return new ManaItemComponent {Mana = Mana};
        }
    }
}