using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectSpawnArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;

    private int _totalSlots;
    private int _takenSlots;
    private int AvailableSlots => _totalSlots - _takenSlots;
    public bool HasEmptySlots => AvailableSlots > 0;
    public bool HasTakenSlots => _takenSlots > 0;

    private void Awake()
    {
        _totalSlots = 0;
        _takenSlots = 0;
        SetObjectsView();
    }

    public void InitSpawnArea(int initTotalSlotsCount)
    {
        _totalSlots = initTotalSlotsCount;
    }

    public bool TryTakeSlot(int needSlotsCount, out int restSlotsCount)
    {
        if (!HasEmptySlots)
        {
            restSlotsCount = needSlotsCount;
            return false;
        }

        var addedSlots = Mathf.Clamp(needSlotsCount, 0, AvailableSlots);
        _takenSlots += addedSlots;

        SetObjectsView();

        restSlotsCount = needSlotsCount - addedSlots;
        return true;
    }

    public void ReleaseSlot()
    {
        _takenSlots = Mathf.Clamp(_takenSlots - 1, 0, _takenSlots);
        SetObjectsView();
    }

    public void ReleaseAllSlots()
    {
        _takenSlots = 0;
        SetObjectsView();
    }

    public void IncreaseTotalSlotsCount()
    {
        _totalSlots++;
    }

    private void SetObjectsView()
    {
        var enabledObjectsCount = Mathf.Clamp(_takenSlots, 0, _objects.Count);
        for (var i = 0; i < _objects.Count; i++)
        {
            _objects[i].SetActive(i < enabledObjectsCount);
        }
    }
}