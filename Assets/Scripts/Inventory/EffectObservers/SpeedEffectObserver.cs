namespace Homework.Inventory
{
    public sealed class SpeedEffectObserver : ItemEffectsObserver<SpeedItemComponent>
    {
        private readonly HeroStats _hero;

        public SpeedEffectObserver(Inventory inventory, EquipmentSystem equipmentsSystem, HeroStats hero) : base(
            inventory, equipmentsSystem)
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