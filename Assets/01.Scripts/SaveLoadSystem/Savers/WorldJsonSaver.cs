using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class WorldJsonSaver : MonoBehaviour
{
    [SerializeField] private World world;
    [ContextMenu("Save World")]
    public void SaveWorld()
    {
        WorldJsonData worldJsonData = new WorldJsonData(world);
        string json = JsonConvert.SerializeObject(worldJsonData);
        System.IO.Directory.CreateDirectory(Application.dataPath + "/SaveData");
        System.IO.File.WriteAllText(Application.dataPath + "/SaveData/World.json", json);
        EntityJsonSaver.SaveEntity(world.entities);
        Debug.Log($"path: {Application.dataPath}/SaveData/World.json");
    }
    [ContextMenu("Load World")]
    public void LoadWorld()
    {
        string json = System.IO.File.ReadAllText(Application.dataPath + "/SaveData/World.json");
        WorldJsonData worldJsonData = JsonUtility.FromJson<WorldJsonData>(json);
        world.Initailize(worldJsonData);
        WorldConfigSO test = null;
        test.Serialize();
    }
}
