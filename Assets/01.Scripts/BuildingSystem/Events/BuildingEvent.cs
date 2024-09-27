using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingEvent : BaseEvent
{
    public BaseBuilding building;
    public BuildingEvent(BaseBuilding building)
    {
        this.building = building;
    }
}
