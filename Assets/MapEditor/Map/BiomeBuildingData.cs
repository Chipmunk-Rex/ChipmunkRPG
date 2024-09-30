using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BiomeBuildingData
{
    [Range(0.01f, 1)] public float spawnRate;
    public BuildingSO buildingSO;
}
