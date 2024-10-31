using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldJsonData : JsonData<World, WorldJsonData>
{
    public List<EntityJsonData> entities = new();
    public List<GroundJsonData> grounds = new();
    public WorldConfigSO worldConfigSO;
    public int seed;

    public override WorldJsonData Serialize(World data)
    {
        foreach (var entity in data.entities)
        {
            entities.Add(new EntityJsonData().Serialize(entity));
        }
        foreach (var ground in data.grounds)
        {
            grounds.Add(new GroundJsonData().Serialize(ground));
        }


        return this;
    }
}
