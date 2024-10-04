using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldEvent : BaseEvent
{
    protected World world;
    public WorldEvent(World world)
    {
        this.world = world;
    }
}
