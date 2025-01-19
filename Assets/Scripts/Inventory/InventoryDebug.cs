using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework.Inventory
{
    public class InventoryDebug : MonoBehaviour
    {
        [ShowInInspector] private EquipmentSystem _equipmentSystem;
        [ShowInInspector] private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory, EquipmentSystem equipmentSystem)
        {
            _inventory = inventory;
            _equipmentSystem = equipmentSystem;
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
            _equipmentSystem.TryEquipItem(itemConfig.GetClone());
        }

        [ShowInInspector]
        public void UnequipItem(InventoryItemConfig itemConfig)
        {
            _equipmentSystem.TryUnequipItem(itemConfig.GetClone());
        }
    }
}