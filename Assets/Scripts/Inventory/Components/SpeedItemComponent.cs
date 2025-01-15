using System;

namespace Homework.Inventory
{
    [Serializable]
    public class SpeedItemComponent : IItemComponent
    {
        public int Speed;

        public IItemComponent Clone()
        {
            return new SpeedItemComponent {Speed = Speed};
        }
    }
}