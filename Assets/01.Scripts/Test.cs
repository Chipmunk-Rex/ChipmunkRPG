using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] BuildingSO so;
    private void Start()
    {
        BuildingManager.Instance.eventContainer.Subscribe(EnumBuildingEvent.CreateBuilding, CreateBuildingHandler);
        BuildingManager.Instance.CreateBuilding(Vector2Int.RoundToInt(transform.position), new BaseBuilding(so));
    }

    private void CreateBuildingHandler(BaseEvent @event)
    {
        @event.onAfterExcute += OnAfterExecute;
        CreateBuildingEvent createBuildingEvent = @event as CreateBuildingEvent;

        Debug.Log("밍밍");
    }

    private void OnAfterExecute(EnumEventResult result)
    {
        Debug.Log(result);
    }
}
