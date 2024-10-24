using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingEvent : WorldEvent
{
    public Building building;

    protected BuildingEvent(World world, Building building) : base(world)
    {
        this.building = building;
    }
}
