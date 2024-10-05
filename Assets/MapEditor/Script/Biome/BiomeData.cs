using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BiomeData
{
    [Range(0.01f, 1)] public float biomeRate;
    public BiomeSO biomeSO;
}
