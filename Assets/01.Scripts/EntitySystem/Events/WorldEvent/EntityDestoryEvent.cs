using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDestoryEvent : WorldEntityEvent
{
    public EntityDestoryEvent(World world, Entity entity) : base(world, entity)
    {
    }

    public override EnumEventResult ExcuteEvent()
    {
        world.entities.Remove(entity);
        GameObject.Destroy(entity.gameObject);
        return EnumEventResult.Successed;
    }
}
