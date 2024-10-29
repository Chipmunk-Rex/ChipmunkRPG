using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityJsonData
{
    public SOAddressData entitySO;
    public Vector2 position;
    public Vector2 lookDir;
    public EntityStats stats;
    public HealthJsonData healthData;
    public EntityJsonData(Entity entity)
    {
        entitySO = entity.entitySO;
        position = entity.transform.position;
        lookDir = entity.lookDir;
        stats = entity.stats;
        healthData = new HealthJsonData(entity.healthCompo);
    }
}
