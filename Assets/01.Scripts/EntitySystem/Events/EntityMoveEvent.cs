using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveEvent : EntityEvent
{
    public Vector2 dir;
    public EntityMoveEvent(Entity entity, Vector2 dir) : base(entity)
    {
        this.dir = dir;
    }

    public override EnumEventResult ExcuteEvent()
    {
        entity.movementCompo.Move(dir);
        return EnumEventResult.Successed;
    }
}
