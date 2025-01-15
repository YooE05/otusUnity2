using System;

namespace Homework.Inventory
{
    [Flags]
    public enum EquipablePlayerParts
    {
        Body = 1,
        RightHand = 2,
        LeftHand = 4,
        Feet = 8,
        Head = 16
    }
}