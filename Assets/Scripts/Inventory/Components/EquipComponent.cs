using System;
using UnityEngine;

namespace Homework.Inventory
{
    [Serializable]
    public class EquipComponent : IItemComponent
    {
        public EquipablePlayerParts EquipableParts;
        [HideInInspector] public int EquippedCount;

        public IItemComponent Clone()
        {
            return new EquipComponent
            {
                EquipableParts = EquipableParts,
                EquippedCount = 0,
            };
        }
    }
}