namespace Homework.Inventory
{
    public sealed class DamageEffectObserver : ItemEffectsObserver
    {
        private readonly HeroStats _hero;

        public DamageEffectObserver(Inventory inventory, HeroStats hero) : base(inventory)
        {
            _hero = hero;
        }

        public override void ApplyItemEffect(InventoryItem item)
        {
            if (item.TryGetComponent(out DamageItemComponent damageComponent))
            {
                _hero.Damage += damageComponent.Damage;
            }
        }

        public override void RemoveItemEffect(InventoryItem item)
        {
            if (item.TryGetComponent(out DamageItemComponent damageComponent))
            {
                _hero.Damage -= damageComponent.Damage;
            }
        }
    }
}