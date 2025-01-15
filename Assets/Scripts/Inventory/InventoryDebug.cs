using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework.Inventory
{
    public class InventoryDebug : MonoBehaviour
    {
        [ShowInInspector] private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        [ShowInInspector]
        public void AddItem(InventoryItemConfig itemConfig)
        {
            _inventory.TryAddItem(itemConfig.GetClone());
        }

        [ShowInInspector]
        public void RemoveItem(InventoryItemConfig itemConfig)
        {
            _inventory.TryRemoveItem(itemConfig.GetClone());
        }

        [ShowInInspector]
        public void EquipItem(InventoryItemConfig itemConfig)
        {
            _inventory.TryEquipItem(itemConfig.GetClone());
        }

        [ShowInInspector]
        public void UnequipItem(InventoryItemConfig itemConfig)
        {
            _inventory.TryUnequipItem(itemConfig.GetClone());
        }
    }
}