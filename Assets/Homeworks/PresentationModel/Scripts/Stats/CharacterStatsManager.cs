using System;
using System.Collections.Generic;
using System.Linq;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStatsManager
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;

        private readonly HashSet<CharacterStat> _stats = new HashSet<CharacterStat>();

        public CharacterStatsManager(List<CharacterStatData> initStats)
        {
            for (int i = 0; i < initStats.Count; i++)
            {
                var stat = new CharacterStat(initStats[i]);
                AddStat(stat);
            }
        }

        public void AddStat(CharacterStat stat)
        {
            if (_stats.Add(stat))
            {
                OnStatAdded?.Invoke(stat);
            }
        }

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

        public void AddStatValue(string name, int value)
        {
            _stats.Any(s => s.Name == name);
            if (_stats.Any(s => s.Name == name))
            {
                var stat = GetStat(name);
                GetStat(name).ChangeValue(stat.Value + value);
            }
            else
            {
                var newStat = new CharacterStat(name, value);
                AddStat(newStat);
            }
        }
    }
}