namespace Homework.Inventory
{
    public static class InventoryUseCases
    {
        public static bool RemoveItem(Inventory inventory, InventoryItem prototype)
        {
            var item = inventory.FindItem(prototype);
            if (item == null) return false;

            item.Count--;
            inventory.ChangeCountNotify(item);

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
    }
}