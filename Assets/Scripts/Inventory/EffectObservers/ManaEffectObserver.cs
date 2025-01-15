namespace Homework.Inventory
{
    public sealed class ManaEffectObserver : ItemEffectsObserver<ManaItemComponent>
    {
        private readonly HeroStats _hero;

        public ManaEffectObserver(Inventory inventory, HeroStats hero) : base(inventory)
        {
            _hero = hero;
        }

        protected override void Apply(ManaItemComponent effectComponent)
        {
            _hero.Mana += effectComponent.Mana;
        }

        protected override void Remove(ManaItemComponent effectComponent)
        {
            _hero.Mana -= effectComponent.Mana;
        }
    }
}