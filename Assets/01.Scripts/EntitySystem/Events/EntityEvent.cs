using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class EntityEvent : BaseEvent
{
    public EntityCompo entity { get; }
    public EntityEvent(EntityCompo entity)
    {
        this.entity = entity;
    }
}
