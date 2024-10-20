using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnEvent : WorldEntityEvent
{
    public Vector2 spawnPos;
    public EntitySpawnEvent(World world, Entity entity) : base(world, entity)
    {
        spawnPos = Vector2.zero;
    }
    public EntitySpawnEvent(World world, Entity entity, Vector2 pos) : base(world, entity)
    {
        this.spawnPos = pos;
    }

    public override EnumEventResult ExcuteEvent()
    {
        try
        {
            world.entities.Add(entity);
            entity.transform.SetParent(world.entityContainerTrm);
            entity.transform.position = spawnPos;
            entity.OnSpawn();
        }
        catch
        {
            return EnumEventResult.Failed;
        }
        return EnumEventResult.Successed;
    }
}
