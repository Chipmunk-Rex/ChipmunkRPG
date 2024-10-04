using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEvent : WorldEvent
{
    protected Entity entity;
    public EntityEvent(World world, Entity entity) : base(world)
    {
        this.entity = entity;
    }
}
