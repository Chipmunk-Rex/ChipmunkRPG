using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public BuildingSO buildingSO { get; private set; }
    // public World currentWorld;
    public Vector2Int pos;
    public Building(BuildingSO buildingSO)
    {
        this.buildingSO = buildingSO;
    }
}
