using System;
using UnityEngine;
using System.ComponentModel;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfo
    {
        public event Action<string> OnNameChanged;
        public event Action<string> OnDescriptionChanged;
        public event Action<Sprite> OnIconChanged; 

        [ReadOnly(true)]
        public string Name { get; private set; }

        [ReadOnly(true)]
        public string Description { get; private set; }

        [ReadOnly(true)]
        public Sprite Icon { get; private set; }

        [ContextMenu("ChangeName")]
        public void ChangeName(string name)
        {
            Name = name;
            OnNameChanged?.Invoke(name);
        }

        [ContextMenu("ChangeDescription")]
        public void ChangeDescription(string description)
        {
            Description = description;
            OnDescriptionChanged?.Invoke(description);
        }

        [ContextMenu("ChangeIcon")]
        public void ChangeIcon(Sprite icon)
        {
           Icon = icon;
           OnIconChanged?.Invoke(icon);
        }
    }
}