using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.UpgradeManager
{
    [Serializable]
    public sealed class StationTimeTable
    {
        [Space] [SerializeField] private float _baseTimeToTransform;

        [Space] [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")] [SerializeField]
        private float[] _levels;

        public float GetTimeToTransform(int level)
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
            var table = new float[maxLevel];
            for (var level = 1; level <= maxLevel; level++)
            {
                var timeToTransform = _baseTimeToTransform / level * 1.2f;
                table[level - 1] = timeToTransform;
            }

            _levels = table;
        }
    }
}