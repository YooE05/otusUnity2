using System;
using System.Collections.Generic;

namespace Homework.Inventory
{
    public static class EquipSystemUseCases
    {
        public static void EquipItem(EquipmentSystem equipmentSystem, Inventory inventory, InventoryItem prototype)
        {
            var item = inventory.FindItem(prototype);
            if (item == null) return;
            if (!item.TryGetComponent<EquipComponent>(out var component)) return;

            var suitParts = GetEquipablePartsList(component);
            if (component.EquippedCount == suitParts.Count || component.EquippedCount == item.Count) return;

            //ищем свободную ячейку
            for (var i = 0; i < suitParts.Count; i++)
            {
                if (equipmentSystem.CheckBodyPartFree(suitParts[i]))
                {
                    component.EquippedCount++;
                    equipmentSystem.EquipBodyPart(suitParts[i], item);
                    return;
                }
            }

            //если все заняты, ищем ячейку, которая занята не текущим предметом
            for (var i = 0; i < suitParts.Count; i++)
            {
                var bodyPartItem = equipmentSystem.GetBodyPartItem(suitParts[i]);
                if (bodyPartItem.Name != item.Name)
                {
                    bodyPartItem.TryGetComponent<EquipComponent>(out var partComponent);
                    partComponent.EquippedCount--;
                    equipmentSystem.UnequipBodyPart(suitParts[i]);

                    component.EquippedCount++;
                    equipmentSystem.EquipBodyPart(suitParts[i], item);
                    return;
                }
            }
        }

        public static void UnequipItem(EquipmentSystem equipmentSystem, Inventory inventory, InventoryItem prototype)
        {
            var item = inventory.FindItem(prototype);
            if (item == null) return;
            if (!item.TryGetComponent<EquipComponent>(out var component)) return;

            var suitParts = GetEquipablePartsList(component);

            for (var i = 0; i < suitParts.Count; i++)
            {
                if (equipmentSystem.CheckBodyPartFree(suitParts[i])) continue;

                if (equipmentSystem.GetBodyPartItem(suitParts[i]).Name == item.Name)
                {
                    component.EquippedCount--;
                    equipmentSystem.UnequipBodyPart(suitParts[i]);
                    return;
                }
            }
        }

        public static List<EquipablePlayerParts> GetEquipablePartsList(EquipComponent resultComponent)
        {
            var suitParts = new List<EquipablePlayerParts>();
            foreach (EquipablePlayerParts part in Enum.GetValues(typeof(EquipablePlayerParts)))
            {
                if ((resultComponent.EquipableParts & part) == part)
                {
                    suitParts.Add(part);
                }
            }

            return suitParts;
        }
    }
}