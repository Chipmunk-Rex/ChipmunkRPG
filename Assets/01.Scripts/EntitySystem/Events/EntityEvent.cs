using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class EntityEvent : BaseEvent
{
    public Entity entity { get; }
    public EntityEvent(Entity entity)
    {
        this.entity = entity;
    }
}
