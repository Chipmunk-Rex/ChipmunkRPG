using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WorldBiomeData
{
    [Range(0.01f, 1)] public float rate;
    public BiomeSO biomeSO;
}
