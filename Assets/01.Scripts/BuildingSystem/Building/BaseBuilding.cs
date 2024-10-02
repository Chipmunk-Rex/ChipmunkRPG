using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding
{
    public BuildingSO buildingSO { get; private set; }
    public World currentWorld;
    public Vector2Int pos;
    public BaseBuilding(BuildingSO buildingSO)
    {
        this.buildingSO = buildingSO;
    }
}
