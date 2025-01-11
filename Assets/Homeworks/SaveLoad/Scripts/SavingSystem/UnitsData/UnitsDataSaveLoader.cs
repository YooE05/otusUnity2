using System.Collections.Generic;
using System.Linq;
using GameEngine;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class UnitsDataSaveLoader : DataSaveLoader<List<UnitData>, UnitManager>
    {
        protected override List<UnitData> ExtractData(UnitManager service)
        {
            var units = service.GetAllUnits().ToList();
            List<UnitData> unitsData = new List<UnitData>();

            for (int i = 0; i < units.Count(); i++)
            {
                var data = new UnitData()
                {
                    Position = units[i].Position,
                    Rotation = units[i].Rotation,
                    HitPoints = units[i].HitPoints,
                    Type = units[i].Type
                };

                unitsData.Add(data);
            }

            return unitsData;
        }

        protected override void SetupData(UnitManager service, List<UnitData> data)
        {
            var currentUnits = service.GetAllUnits().ToList();
            for (int i = 0; i < currentUnits.Count(); i++)
            {
                if (currentUnits[i] != null)
                {
                    service.DestroyUnit(currentUnits[i]);
                }
            }

            HashSet<Unit> newUnits = new HashSet<Unit>();

            for (int i = 0; i < data.Count; i++)
            {
                var unitPrefab = Resources.Load<Unit>($"Prefabs/UnitObjects/{data[i].Type}");

                var unit = service.SpawnUnit(unitPrefab, data[i].Position, Quaternion.Euler(data[i].Rotation));
                unit.HitPoints = data[i].HitPoints;

                newUnits.Add(unit);
            }

            service.SetupUnits(newUnits);
        }
    }
}