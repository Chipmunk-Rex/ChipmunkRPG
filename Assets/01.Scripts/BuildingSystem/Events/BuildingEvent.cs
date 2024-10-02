using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingEvent : WorldEvent
{
    public BaseBuilding building;

    protected BuildingEvent(World world, BaseBuilding building) : base(world)
    {
        this.building = building;
    }
}
