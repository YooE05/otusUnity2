using System;

namespace Homework.Inventory
{
    public interface IItemComponent
    {
        public IItemComponent Clone();
    }

    [Serializable]
    public class StackComponent : IItemComponent
    {
        public int Count = 0;
        public int MaxCount = 5;

        public IItemComponent Clone()
        {
            return new StackComponent
            {
                Count = Count,
                MaxCount = MaxCount
            };
        }
    }

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