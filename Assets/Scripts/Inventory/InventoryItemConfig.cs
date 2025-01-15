using UnityEngine;

namespace Homework.Inventory
{
    [CreateAssetMenu(fileName = "InventoryItemConfig", menuName = "Configs/InventoryItemConfig")]
    public class InventoryItemConfig : ScriptableObject
    {
        public InventoryItem Prototype;

        public InventoryItem GetClone()
        {
            return Prototype.Clone();
        }
    }
}