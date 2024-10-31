using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingJsonData : JsonData<Building, BuildingJsonData>
{
    public override BuildingJsonData Serialize(Building data)
    {
        return this;
    }
}
