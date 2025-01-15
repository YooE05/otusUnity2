namespace Homework.Inventory
{
    public interface IItemEffectsObserver
    {
        public void ApplyItemEffect(InventoryItem item);
        public void RemoveItemEffect(InventoryItem item);
    }
}