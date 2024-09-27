using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    EventMediatorContainerType eventContainer = new();
    private void Awake()
    {
        eventContainer.Subscribe<CreateBuildingEvent>(CreateBuildingHandler);
    }

    private void CreateBuildingHandler(BaseEvent @event)
    {
        CreateBuildingEvent createBuildingEvent = @event as CreateBuildingEvent;

        // createBuildingEvent.debugString = "밍밍";
    }

    private void Start()
    {
        eventContainer.Invoke<CreateBuildingEvent>(new CreateBuildingEvent());
    }
}
