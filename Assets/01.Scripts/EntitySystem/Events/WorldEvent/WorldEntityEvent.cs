using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldEntityEvent : WorldEvent
{
    protected Entity entity;
    public WorldEntityEvent(World world, Entity entity) : base(world)
    {
        this.entity = entity;
    }
}
