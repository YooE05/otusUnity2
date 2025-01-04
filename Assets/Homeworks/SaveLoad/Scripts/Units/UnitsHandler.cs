using System.Collections.Generic;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class UnitsHandler
    {
        private readonly List<UnitData> _unitsData = new List<UnitData>();
        private readonly List<GameObject> _unitsGOs;

        public UnitsHandler(EntitiesContainer entitiesContainer)
        {
            _unitsGOs = entitiesContainer.UnitsGOs;
        }

        public UnitData[] GetAllUnitsData()
        {
            _unitsData.Clear();

            for (int i = 0; i < _unitsGOs.Count; i++)
            {
                UnitData newUnitData;

                if (_unitsGOs[i] == null)
                {
                    newUnitData = GetDefaultData();
                    _unitsData.Add(newUnitData);
                    continue;
                }

                var component = _unitsGOs[i].GetComponent<UnitObject>();

                newUnitData = new UnitData
                {
                    Position = _unitsGOs[i].transform.position,
                    Rotation = _unitsGOs[i].transform.rotation,
                    Speed = component.Speed,
                    Damage = component.Damage,
                    HitPoints = component.HitPoints
                };

                _unitsData.Add(newUnitData);
            }

            return _unitsData.ToArray();
        }

        public void SetupUnits(UnitData[] data)
        {
            for (int i = 0; i < _unitsGOs.Count; i++)
            {
                if (_unitsGOs[i] == null) continue;

                _unitsGOs[i].transform.position = data[i].Position;
                _unitsGOs[i].transform.rotation = data[i].Rotation;

                var unitStats = _unitsGOs[i].GetComponent<UnitObject>();
                unitStats.Damage = data[i].Damage;
                unitStats.Speed = data[i].Speed;
                unitStats.HitPoints = data[i].HitPoints;
            }
        }

        private UnitData GetDefaultData()
        {
            var data = new UnitData
            {
                Position = Vector3.zero,
                Rotation = Quaternion.identity,
                Speed = 0,
                Damage = 0,
                HitPoints = 0
            };
            
            return data;
        }
    }
}