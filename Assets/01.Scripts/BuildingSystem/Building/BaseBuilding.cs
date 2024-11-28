using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : INDSerializeAble
{
    public BuildingSO buildingSO { get; private set; }
    // public World currentWorld;
    public Vector2Int pos;
    /// <summary>
    /// 건물의 Entity 스폰은 CreateBuildingEvent에서 처리
    /// </summary>
    public Entity buildingEntity;
    public Building(BuildingSO buildingSO)
    {
        this.buildingSO = buildingSO;
        if (buildingSO.buildingEntitySO != null)
        {
            buildingEntity = buildingSO.buildingEntitySO.CreateEntity();
            buildingEntity.parentBuilding = this;
            buildingEntity.hasOwner = true;
        }
    }

    public NDSData Serialize()
    {
        NDSData data = new NDSData();
        data.AddData("buildingSO", SOAddressSO.Instance.GetIDBySO(buildingSO));
        data.AddData("pos", new JsonVector2(pos));
        data.AddData("buildingEntity", buildingEntity?.Serialize());
        return data;
        // throw new NotImplementedException();
    }

    public void Deserialize(NDSData data)
    {
        pos = data.GetData<JsonVector2>("pos");
        buildingSO = SOAddressSO.Instance.GetSOByID<BuildingSO>(uint.Parse(data.GetDataString("buildingSO")));
        if (buildingSO.buildingEntitySO != null)
        {
            buildingEntity = buildingSO.buildingEntitySO.CreateEntity();
            buildingEntity.Deserialize(data.GetData<NDSData>("buildingEntity"));
            buildingEntity.parentBuilding = this;
            if(buildingEntity == null)
            {
                Debug.Log("Building Entity is null");
                return;
            }
            buildingEntity.hasOwner = true;
            buildingEntity.SpawnEntity(pos: pos);
        }
        // throw new NotImplementedException();
    }
}
