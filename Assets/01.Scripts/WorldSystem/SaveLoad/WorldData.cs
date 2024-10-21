using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public string worldName;
    public WorldConfigSO worldConfig;
    public List<Entity> entities = new();
    public SerializableDictionary<Vector2Int, Ground> groundDatas;
}
