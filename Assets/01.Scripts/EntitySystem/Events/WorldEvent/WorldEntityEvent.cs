using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldEntityEvent : WorldEvent
{
    protected EntityCompo entity;
    public WorldEntityEvent(World world, EntityCompo entity) : base(world)
    {
        this.entity = entity;
    }
}
