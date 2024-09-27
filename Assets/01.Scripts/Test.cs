using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] BuildingSO so;
    private void Awake()
    {
        BuildingManager.Instance.CreateBuilding(Vector2Int.zero, new BaseBuilding(so));
        BuildingManager.Instance.eventContainer.Subscribe(EnumBuildingEvent.CreateBuilding, CreateBuildingHandler);
    }

    private void CreateBuildingHandler(BaseEvent @event)
    {
        CreateBuildingEvent createBuildingEvent = @event as CreateBuildingEvent;

        Debug.Log("밍밍");
    }
}
