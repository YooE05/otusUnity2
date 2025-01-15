using System;

namespace Homework.Inventory
{
    [Serializable]
    public class EquipComponent : IItemComponent
    {
        public EquipablePlayerParts EquipableParts;
        public bool IsEquipped;
        public int EquippedCount;

        public IItemComponent Clone()
        {
            return new EquipComponent
            {
                EquipableParts = EquipableParts,
                EquippedCount = 0,
                IsEquipped = IsEquipped,
            };
        }
    }
}