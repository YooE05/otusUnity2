namespace Homework.Inventory
{
    public sealed class SpeedEffectObserver : ItemEffectsObserver<SpeedItemComponent>
    {
        private readonly HeroStats _hero;

        public SpeedEffectObserver(Inventory inventory, HeroStats hero) : base(inventory)
        {
            _hero = hero;
        }

        protected override void Apply(SpeedItemComponent effectComponent)
        {
            _hero.Speed += effectComponent.Speed;
        }

        protected override void Remove(SpeedItemComponent effectComponent)
        {
            _hero.Speed -= effectComponent.Speed;
        }
    }
}