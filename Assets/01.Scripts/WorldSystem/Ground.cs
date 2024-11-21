using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ground : INDSerializeAble
{
    public Vector2Int WorldPos => worldPos;
    Vector2Int worldPos;
    public GroundSO groundSO;
    public BiomeSO biomeSO;
    public Building building;
    public Ground(Vector2Int worldPos, GroundSO groundSO, BiomeSO biome, Building building = null)
    {
        this.worldPos = worldPos;
        this.groundSO = groundSO;
        this.biomeSO = biome;
        this.building = building;
    }

    public NDSData Serialize()
    {
        NDSData data = new NDSData();
        JsonVector2 jsonVector2 = worldPos;
        data.AddData("worldPos", jsonVector2);
        data.AddData("groundSO", SOAddressSO.Instance.GetIDBySO(groundSO));
        data.AddData("biomeSO", SOAddressSO.Instance.GetIDBySO(biomeSO));
        if (building != null)
            data.AddData("building", building.Serialize());
        return data;
    }

    public void Deserialize(NDSData data)
    {
        worldPos = data.GetData<JsonVector2>("worldPos");
        groundSO = SOAddressSO.Instance.GetSOByID<GroundSO>(uint.Parse(data.GetDataString("groundSO")));
        biomeSO = SOAddressSO.Instance.GetSOByID<BiomeSO>(uint.Parse(data.GetDataString("biomeSO")));

        if (data.Data.ContainsKey("building"))
        {
            NDSData buldingNDSData = data.GetData<NDSData>("building");
            BuildingSO buildingSO = SOAddressSO.Instance.GetSOByID<BuildingSO>(uint.Parse(buldingNDSData.GetDataString("buildingSO")));
            building = buildingSO.CreateBuilding();
            building.Deserialize(buldingNDSData);
        }
    }
}
