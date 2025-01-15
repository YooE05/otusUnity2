namespace Homework.Inventory
{
    public sealed class DamageEffectObserver : ItemEffectsObserver<DamageItemComponent>
    {
        private readonly HeroStats _hero;

        public DamageEffectObserver(Inventory inventory, HeroStats hero) : base(inventory)
        {
            _hero = hero;
        }

        protected override void Apply(DamageItemComponent effectComponent)
        {
            _hero.Damage += effectComponent.Damage;
        }

        protected override void Remove(DamageItemComponent effectComponent)
        {
            _hero.Damage -= effectComponent.Damage;
        }
    }
}