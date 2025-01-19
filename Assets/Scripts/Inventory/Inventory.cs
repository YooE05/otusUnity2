using System;
using System.Collections.Generic;

namespace Homework.Inventory
{
    [Serializable]
    public class Inventory
    {
        public event Action<InventoryItem> OnChangeItemCount;
        public event Action<InventoryItem> OnItemConsumed;

        public List<InventoryItem> StorageItems = new List<InventoryItem>();

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

        public void ChangeCountNotify(InventoryItem item)
        {
            OnChangeItemCount?.Invoke(item);
        }

        public void TryConsumeItem(InventoryItem prototype)
        {
            InventoryUseCases.ConsumeItem(this, prototype);
        }

        public void ConsumeNotify(InventoryItem item)
        {
            OnItemConsumed?.Invoke(item);
        }
    }
}