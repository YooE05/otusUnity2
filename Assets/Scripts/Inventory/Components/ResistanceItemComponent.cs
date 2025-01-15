using System;

namespace Homework.Inventory
{
    [Serializable]
    public class ResistanceItemComponent : IItemComponent
    {
        public int Resistance;

        public IItemComponent Clone()
        {
            return new ResistanceItemComponent {Resistance = Resistance};
        }
    }
}