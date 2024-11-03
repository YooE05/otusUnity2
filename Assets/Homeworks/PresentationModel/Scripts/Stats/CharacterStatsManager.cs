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
        private readonly int _increaseStatPercent;

        public CharacterStatsManager(List<CharacterStatData> initStats, int increaseStatPercent)
        {
            _increaseStatPercent = increaseStatPercent;
            for (int i = 0; i < initStats.Count; i++)
            {
                var stat = new CharacterStat(initStats[i]);
                AddStat(stat);
            }
        }

        private void AddStat(CharacterStat stat)
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
            var any = _stats.Any(s => s.Name == name);
            if (any)
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

        public void IncreaseAllStats()
        {
            foreach (var stat in _stats)
            {
                stat.ChangeValue((int) (stat.Value * (1 + _increaseStatPercent / 100f)));
            }
        }
    }
}