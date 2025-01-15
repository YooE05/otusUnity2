using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Homework.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public string Name;
        public int Count = 1;
        public ItemMetadata Metadata;
        public ItemFlags Flags;

        [SerializeReference] public IItemComponent[] ItemComponents;

        public InventoryItem Clone()
        {
            var components = CloneComponents();
            var prototype = new InventoryItem
            {
                Name = Name,
                Count = 1,
                Metadata = CloneMetadata(),
                Flags = Flags,
                ItemComponents = components,
            };

            return prototype;
        }

        private ItemMetadata CloneMetadata()
        {
            return new ItemMetadata
            {
                Icon = Metadata.Icon,
                Description = Metadata.Description
            };
        }

        private IItemComponent[] CloneComponents()
        {
            var list = new List<IItemComponent>();

            for (int i = 0; i < ItemComponents.Length; i++)
            {
                list.Add(ItemComponents[i].Clone());
            }

            return list.ToArray();
        }

        public T GetComponent<T>() where T : IItemComponent
        {
            for (int i = 0; i < ItemComponents.Count(); i++)
            {
                if (ItemComponents[i] is T component)
                {
                    return component;
                }
            }

            return default;
        }

        public bool TryGetComponent<T>(out T resultComponent) where T : IItemComponent
        {
            for (int i = 0; i < ItemComponents.Count(); i++)
            {
                if (ItemComponents[i] is T component)
                {
                    resultComponent = component;
                    return true;
                }
            }

            resultComponent = default;
            return false;
        }
    }
}