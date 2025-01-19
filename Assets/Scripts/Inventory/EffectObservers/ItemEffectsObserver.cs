namespace Homework.Inventory
{
    public abstract class ItemEffectsObserver<TEffectComponent> : IItemEffectsObserver
        where TEffectComponent : IItemComponent
    {
        private readonly Inventory _inventory;
        private readonly EquipmentSystem _equipmentsSystem;

        protected ItemEffectsObserver(Inventory inventory, EquipmentSystem equipmentsSystem)
        {
            _inventory = inventory;
            _equipmentsSystem = equipmentsSystem;

            _inventory.OnItemConsumed += ApplyItemEffect;

            _equipmentsSystem.OnItemEqiped += ApplyItemEffect;
            _equipmentsSystem.OnItemUneqiped += RemoveItemEffect;
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
            _equipmentsSystem.OnItemEqiped -= ApplyItemEffect;
            _equipmentsSystem.OnItemUneqiped -= RemoveItemEffect;

            _inventory.OnItemConsumed -= ApplyItemEffect;
        }
    }
}