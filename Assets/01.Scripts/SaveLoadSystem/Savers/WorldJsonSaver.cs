using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class WorldJsonSaver : MonoBehaviour
{
    [SerializeField] private World world;
    [ContextMenu("Save World")]
    public void SaveWorld()
    {
        WorldJsonData worldJsonData = new WorldJsonData().Serialize(world);
        Debug.Log(worldJsonData.entities[0]);
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
    [ContextMenu("Load World")]
    public void LoadWorld()
    {
        WorldJsonData worldJsonData = null;
        string json = System.IO.File.ReadAllText(Application.dataPath + "/SaveData/World.json");
        worldJsonData = JsonConvert.DeserializeObject<WorldJsonData>(json,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }
        );
        // world.Load(worldJsonData);
        world.Initailize(worldJsonData);
        // List<EntityJsonData> entityDatas = worldJsonData.entities;
        // foreach(EntityJsonData entityData in entityDatas)
        // {
        //     ScriptableObject scriptableObject = entityData.entitySO;
        //     Debug.Log(scriptableObject.name);
        //     Entity entity = PoolManager.Instance.Pop("Entity").GetComponent<Entity>();
        //     entity.Initailize(entityData);
        // }

    }
}
