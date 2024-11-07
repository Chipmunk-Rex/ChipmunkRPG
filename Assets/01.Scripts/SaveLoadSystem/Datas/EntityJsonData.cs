using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityJsonData : JsonData<Entity, EntityJsonData>
{
    public SOAddressData<EntitySO> entitySO;
    public JsonVector2 position;
    public JsonVector2 lookDir;
    public EntityStats stats;
    public int currentHealth;
    public int hp;
    public override EntityJsonData Serialize(Entity entity)
    {
        entitySO = entity.EntitySO;
        position = entity.transform.position;
        lookDir = entity.lookDir;
        stats = entity.stats;
        hp = entity.hp;
        return this;
    }
}
