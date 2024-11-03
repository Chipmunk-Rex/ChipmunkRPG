using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class WorldJsonSaver : JsonSaver
{
    [SerializeField] private World world;
    [ContextMenu("JsonSave")]
    public override void Save()
    {
        WorldJsonData worldJsonData = new WorldJsonData().Serialize(world);
        string json = JsonConvert.SerializeObject(worldJsonData,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }
        );
        System.IO.Directory.CreateDirectory(Application.dataPath + "/SaveData");
        System.IO.File.WriteAllText(Application.dataPath + "/SaveData/World.json", json);
        EntityJsonSaver.SaveEntity(world.entities);
        Debug.Log($"path: {Application.dataPath}/SaveData/World.json");
    }
    [ContextMenu("JsonLoad")]
    public override void Load()
    {
        WorldJsonData worldJsonData = null;
        string json = System.IO.File.ReadAllText(Application.dataPath + "/SaveData/World.json");
        worldJsonData = JsonConvert.DeserializeObject<WorldJsonData>(json,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }
        );
        world.Initailize(worldJsonData);

    }
}
