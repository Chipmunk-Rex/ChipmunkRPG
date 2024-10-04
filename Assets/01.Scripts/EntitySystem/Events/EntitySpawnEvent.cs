using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnEvent : WorldEvent
{
    Entity entity;
    public EntitySpawnEvent(World world, Entity entity) : base(world)
    {
        this.entity = entity;
    }

    public override EnumEventResult ExcuteEvent()
    {
        try
        {
            world.entities.Add(entity);
            entity.transform.SetParent(world.entityContainerTrm);
        }
        catch
        {
            return EnumEventResult.Failed;
        }
        return EnumEventResult.Successed;
    }
}
