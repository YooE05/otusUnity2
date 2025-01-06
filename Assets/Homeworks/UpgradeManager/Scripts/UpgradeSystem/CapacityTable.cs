using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.UpgradeManager
{
    [Serializable]
    public class CapacityTable
    {
        [Space] [SerializeField] private int _baseCapacity;

        [Space] [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")] [SerializeField]
        private int[] _levels;

        public int GetCapacity(int level)
        {
            var index = level - 1;
            index = Mathf.Clamp(index, 0, this._levels.Length - 1);
            return _levels[index];
        }

        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level #{index + 1}");
        }

        public void OnValidate(int maxLevel)
        {
            EvaluateSpeedTable(maxLevel);
        }

        private void EvaluateSpeedTable(int maxLevel)
        {
            var table = new int[maxLevel];
            table[0] = _baseCapacity;
            for (var level = 2; level <= maxLevel; level++)
            {
                var timeToTransform = _baseCapacity + (int) (level * 1.5);
                table[level - 1] = timeToTransform;
            }

            _levels = table;
        }
    }
}