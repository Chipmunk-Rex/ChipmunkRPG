using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnEvent : EntityEvent
{
    public EntitySpawnEvent(World world, Entity entity) : base(world, entity)
    {
    }

    public override EnumEventResult ExcuteEvent()
    {
        try
        {
            world.entities.Add(entity);
            entity.transform.SetParent(world.entityContainerTrm);
            entity.OnSpawn();
        }
        catch
        {
            return EnumEventResult.Failed;
        }
        return EnumEventResult.Successed;
    }
}
