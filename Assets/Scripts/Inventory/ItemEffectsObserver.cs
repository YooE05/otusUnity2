namespace Homework.Inventory
{
    public abstract class ItemEffectsObserver : IItemEffectsObserver
    {
        private readonly Inventory _inventory;

        protected ItemEffectsObserver(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.OnItemEqiped += ApplyItemEffect;
            _inventory.OnItemUneqiped += RemoveItemEffect;

            _inventory.OnItemConsumed += ApplyItemEffect;
        }

        public virtual void ApplyItemEffect(InventoryItem item)
        {
        }

        public virtual void RemoveItemEffect(InventoryItem item)
        {
        }

        ~ItemEffectsObserver()
        {
            _inventory.OnItemEqiped -= ApplyItemEffect;
            _inventory.OnItemUneqiped -= RemoveItemEffect;

            _inventory.OnItemConsumed -= ApplyItemEffect;
        }
    }
}