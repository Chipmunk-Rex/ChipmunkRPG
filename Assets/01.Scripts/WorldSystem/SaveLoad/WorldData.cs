using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public string worldName;
    public WorldConfigSO worldConfig;
    public List<Entity> entities = new();
    // public GroundData groundData;
    public GroundData[] groundDatas;
}
