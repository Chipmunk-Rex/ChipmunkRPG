using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundJsonData : JsonData<KeyValuePair<Vector2Int, Ground>, GroundJsonData>
{
    public JsonVector2 worldPos;
    public SOAddressData<GroundSO> groundSO;
    public SOAddressData<BiomeSO> biomeSO;

    public override GroundJsonData Serialize(KeyValuePair<Vector2Int, Ground> data)
    {
        worldPos = data.Key;
        groundSO = data.Value.groundSO;
        biomeSO = data.Value.biome;
        return this;
    }
}
