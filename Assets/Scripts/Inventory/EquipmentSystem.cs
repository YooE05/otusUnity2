using System;
using System.Collections.Generic;

namespace Homework.Inventory
{
    [Serializable]
    public sealed class EquipmentSystem
    {
        public event Action<InventoryItem> OnItemEqiped;
        public event Action<InventoryItem> OnItemUneqiped;

        public Dictionary<EquipablePlayerParts, InventoryItem> EquipedItems =
            new Dictionary<EquipablePlayerParts, InventoryItem>();

        private Inventory _inventory;

        public EquipmentSystem(Inventory inventory)
        {
            _inventory = inventory;

            foreach (EquipablePlayerParts part in Enum.GetValues(typeof(EquipablePlayerParts)))
            {
                EquipedItems[part] = null;
            }

            _inventory.OnChangeItemCount += CheckUnequipmentNeed;
        }

        public void TryEquipItem(InventoryItem prototype)
        {
            EquipSystemUseCases.EquipItem(this, _inventory, prototype);
        }

        public void EquipBodyPart(EquipablePlayerParts bodyPart, InventoryItem item)
        {
            EquipedItems[bodyPart] = item;
            OnItemEqiped?.Invoke(item);
        }

        public void TryUnequipItem(InventoryItem prototype)
        {
            EquipSystemUseCases.UnequipItem(this, _inventory, prototype);
        }

        public void UnequipBodyPart(EquipablePlayerParts bodyPart)
        {
            var item = EquipedItems[bodyPart];
            if (item == null) return;

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

        private void CheckUnequipmentNeed(InventoryItem item)
        {
            item.TryGetComponent<EquipComponent>(out var component);
            if (component == null) return;

            if (item.Count < component.EquippedCount)
            {
                EquipSystemUseCases.UnequipItem(this, _inventory, item);
            }
        }

        ~EquipmentSystem()
        {
            _inventory.OnChangeItemCount -= CheckUnequipmentNeed;
        }
    }
}