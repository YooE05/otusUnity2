namespace Homework.Inventory
{
    public sealed class ResistanceEffectObserver : ItemEffectsObserver<ResistanceItemComponent>
    {
        private readonly HeroStats _hero;

        public ResistanceEffectObserver(Inventory inventory, EquipmentSystem equipmentsSystem, HeroStats hero) : base(
            inventory, equipmentsSystem)
        {
            _hero = hero;
        }

        protected override void Apply(ResistanceItemComponent effectComponent)
        {
            _hero.Resistance += effectComponent.Resistance;
        }

        protected override void Remove(ResistanceItemComponent effectComponent)
        {
            _hero.Resistance -= effectComponent.Resistance;
        }
    }
}