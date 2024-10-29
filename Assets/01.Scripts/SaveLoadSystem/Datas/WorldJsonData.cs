using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldJsonData
{
    public List<EntityJsonData> entities;
    public WorldConfigSO worldConfigSO;
    public int seed;
    public WorldJsonData(World world)
    {
        entities = new List<EntityJsonData>();
        foreach (var entity in world.entities)
        {
            entities.Add(new EntityJsonData(entity));
        }
    }
}
