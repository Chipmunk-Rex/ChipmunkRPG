using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : INDSerializeAble
{
    public BuildingSO buildingSO { get; private set; }
    // public World currentWorld;
    public Vector2Int pos;
    public Building(BuildingSO buildingSO)
    {
        this.buildingSO = buildingSO;
    }

    public NDSData Serialize()
    {
        throw new NotImplementedException();
    }

    public void Deserialize(NDSData data)
    {
        throw new NotImplementedException();
    }
}
