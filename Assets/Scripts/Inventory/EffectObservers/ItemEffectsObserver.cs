namespace Homework.Inventory
{
    public abstract class ItemEffectsObserver<TEffectComponent> : IItemEffectsObserver
        where TEffectComponent : IItemComponent
    {
        private readonly Inventory _inventory;

        protected ItemEffectsObserver(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.OnItemEqiped += ApplyItemEffect;
            _inventory.OnItemUneqiped += RemoveItemEffect;

            _inventory.OnItemConsumed += ApplyItemEffect;
        }

        public void ApplyItemEffect(InventoryItem item)
        {
            if (item.TryGetComponent(out TEffectComponent component))
            {
                Apply(component);
            }
        }

        public void RemoveItemEffect(InventoryItem item)
        {
            if (item.TryGetComponent(out TEffectComponent component))
            {
                Remove(component);
            }
        }

        protected abstract void Apply(TEffectComponent effectComponent);
        protected abstract void Remove(TEffectComponent effectComponent);

        ~ItemEffectsObserver()
        {
            _inventory.OnItemEqiped -= ApplyItemEffect;
            _inventory.OnItemUneqiped -= RemoveItemEffect;

            _inventory.OnItemConsumed -= ApplyItemEffect;
        }
    }
}