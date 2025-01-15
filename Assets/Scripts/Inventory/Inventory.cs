using System;
using System.Collections.Generic;

namespace Homework.Inventory
{
    [Serializable]
    public sealed class Inventory
    {
        public event Action<InventoryItem> OnItemEqiped;
        public event Action<InventoryItem> OnItemUneqiped;

        public event Action<InventoryItem> OnItemConsumed;

        public List<InventoryItem> StorageItems = new List<InventoryItem>();

        public Dictionary<EquipablePlayerParts, InventoryItem> EquipedItems =
            new Dictionary<EquipablePlayerParts, InventoryItem>();

        public Inventory()
        {
            foreach (EquipablePlayerParts part in Enum.GetValues(typeof(EquipablePlayerParts)))
            {
                EquipedItems[part] = null;
            }
        }

        public InventoryItem FindItem(InventoryItem prototype)
        {
            return StorageItems.Find(item => item.Name == prototype.Name);
        }

        public void TryAddItem(InventoryItem prototype)
        {
            InventoryUseCases.AddItem(this, prototype);
        }

        public void AddItem(InventoryItem item)
        {
            StorageItems.Add(item);
        }

        public void TryRemoveItem(InventoryItem prototype)
        {
            InventoryUseCases.RemoveItem(this, prototype);
        }

        public void RemoveItem(InventoryItem item)
        {
            StorageItems.Remove(item);
        }

        public void TryConsumeItem(InventoryItem prototype)
        {
            InventoryUseCases.ConsumeItem(this, prototype);
        }

        public void ConsumeNotify(InventoryItem item)
        {
            OnItemConsumed?.Invoke(item);
        }

        //Equipment
        public void TryEquipItem(InventoryItem prototype)
        {
            InventoryUseCases.EquipItem(this, prototype);
        }

        public void EquipBodyPart(EquipablePlayerParts bodyPart, InventoryItem item)
        {
            EquipedItems[bodyPart] = item;
            OnItemEqiped?.Invoke(item);
        }

        public void TryUnequipItem(InventoryItem prototype)
        {
            InventoryUseCases.UnequipItem(this, prototype);
        }

        public void UnequipBodyPart(EquipablePlayerParts bodyPart)
        {
            var item = EquipedItems[bodyPart];
            if (item == null) return;

            item.TryGetComponent<EquipComponent>(out var component);
            if (component.EquippedCount == 0)
            {
                component.IsEquipped = false;
            }

            EquipedItems[bodyPart] = null;
            OnItemUneqiped?.Invoke(item);
        }

        public bool CheckBodyPartFree(EquipablePlayerParts bodyPart)
        {
            return EquipedItems[bodyPart] == null;
        }

        public InventoryItem GetBodyPartItem(EquipablePlayerParts bodyPart)
        {
            return EquipedItems[bodyPart];
        }
    }

    public static class InventoryUseCases
    {
        public static bool RemoveItem(Inventory inventory, InventoryItem prototype)
        {
            var item = inventory.FindItem(prototype);
            if (item == null) return false;

            item.TryGetComponent<EquipComponent>(out var component);
            if (component != null)
            {
                if (component.EquippedCount == item.Count)
                {
                    UnequipItem(inventory, item);
                }
            }

            item.Count--;

            if (item.Count == 0)
            {
                inventory.RemoveItem(item);
            }

            return true;
        }

        public static void AddItem(Inventory inventory, InventoryItem prototype)
        {
            var item = inventory.FindItem(prototype);
            if (item != null)
            {
                item.Count++;
            }
            else
            {
                inventory.AddItem(prototype);
            }
        }

        public static void ConsumeItem(Inventory inventory, InventoryItem prototype)
        {
            if ((prototype.Flags & ItemFlags.Consumable) == 0) return;

            if (RemoveItem(inventory, prototype))
            {
                inventory.ConsumeNotify(prototype);
            }
        }

        public static void EquipItem(Inventory inventory, InventoryItem prototype)
        {
            var item = inventory.FindItem(prototype);
            if (item == null) return;
            if (!item.TryGetComponent<EquipComponent>(out var component)) return;

            var suitParts = GetEquipablePartsList(component);
            if (component.IsEquipped &&
                (component.EquippedCount == suitParts.Count || component.EquippedCount == item.Count)) return;

            //ищем свободную ячейку
            for (var i = 0; i < suitParts.Count; i++)
            {
                if (inventory.CheckBodyPartFree(suitParts[i]))
                {
                    component.IsEquipped = true;
                    component.EquippedCount++;
                    inventory.EquipBodyPart(suitParts[i], item);
                    return;
                }
            }

            //если все заняты, ищем ячейку, которая занята не текущим предметом
            for (var i = 0; i < suitParts.Count; i++)
            {
                var bodyPartItem = inventory.GetBodyPartItem(suitParts[i]);
                if (bodyPartItem.Name != item.Name)
                {
                    bodyPartItem.TryGetComponent<EquipComponent>(out var partComponent);
                    partComponent.EquippedCount--;
                    inventory.UnequipBodyPart(suitParts[i]);

                    component.IsEquipped = true;
                    component.EquippedCount++;
                    inventory.EquipBodyPart(suitParts[i], item);
                    return;
                }
            }
        }

        public static void UnequipItem(Inventory inventory, InventoryItem prototype)
        {
            var item = inventory.FindItem(prototype);
            if (item == null) return;
            if (!item.TryGetComponent<EquipComponent>(out var component)) return;
            if (!component.IsEquipped) return;

            var suitParts = GetEquipablePartsList(component);

            for (var i = 0; i < suitParts.Count; i++)
            {
                if (inventory.CheckBodyPartFree(suitParts[i])) continue;

                if (inventory.GetBodyPartItem(suitParts[i]).Name == item.Name)
                {
                    component.EquippedCount--;
                    inventory.UnequipBodyPart(suitParts[i]);
                    return;
                }
            }
        }

        private static List<EquipablePlayerParts> GetEquipablePartsList(EquipComponent resultComponent)
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