using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldJsonData : JsonData<World, WorldJsonData>
{
    public List<EntityJsonData> entities = new();
    public List<GroundJsonData> grounds = new();
    public SOAddressData<WorldConfigSO> worldConfigSO;
    public int seed;

    public override WorldJsonData Serialize(World world)
    {
        worldConfigSO = world.worldSO;
        seed = world.seed;
        foreach (var entity in world.entities)
        {
            entities.Add(new EntityJsonData().Serialize(entity));
        }
        foreach (var ground in world.grounds)
        {
            grounds.Add(new GroundJsonData().Serialize(ground));
        }


        return this;
    }
}
