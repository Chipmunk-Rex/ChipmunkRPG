using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
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
        Debug.Log("Spawn Entity");
        try
        {
            if (world.entities.Contains(entity))
            {
                Debug.LogError("Entity already exist in world");
                return EnumEventResult.Failed;
            }
            if (entity.entityCompo == null)
            {
                Debug.LogWarning("EntityCompo is null, create new one");
                EntityCompo entityCompo = PoolManager.Instance.Pop("Entity").GetComponent<EntityCompo>();
                entity.entityCompo = entityCompo;
            }

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
