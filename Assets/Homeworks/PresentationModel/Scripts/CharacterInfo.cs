using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfo
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;

        //[ShowInInspector]
        private readonly HashSet<CharacterStat> _stats = new HashSet<CharacterStat>();

        [ContextMenu("AddStat")]
        public void AddStat(CharacterStat stat)
        {
            if (_stats.Add(stat))
            {
                OnStatAdded?.Invoke(stat);
            }
        }

        [ContextMenu("RemoveStat")]
        public void RemoveStat(CharacterStat stat)
        {
            if (_stats.Remove(stat))
            {
                OnStatRemoved?.Invoke(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in _stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return _stats.ToArray();
        }
    }
}