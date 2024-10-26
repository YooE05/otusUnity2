using System;
using System.ComponentModel;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel
    {
        public event Action OnLevelUp;
        public event Action<int> OnExperienceChanged;

        [ReadOnly(true)]
        public int CurrentLevel { get; private set; } = 1;

        [ReadOnly(true)]
        public int CurrentExperience { get; private set; }

        [ReadOnly(true)]
        public int RequiredExperience
        {
            get { return 100 * (CurrentLevel + 1); }
        }

        [ContextMenu("AddExperience")]
        public void AddExperience(int range)
        {
            var xp = Math.Min(CurrentExperience + range, RequiredExperience);
            CurrentExperience = xp;
            OnExperienceChanged?.Invoke(xp);
        }

        [ContextMenu("LevelUp")]
        public void LevelUp()
        {
            if (CanLevelUp())
            {
                CurrentExperience = 0;
                CurrentLevel++;
                OnLevelUp?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return CurrentExperience == RequiredExperience;
        }
    }
}