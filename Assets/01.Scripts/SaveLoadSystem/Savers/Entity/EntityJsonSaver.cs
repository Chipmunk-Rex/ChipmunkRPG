using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityJsonSaver
{
    public static void SaveEntity(List<Entity> entity)
    {
        EntityJsonData[] entityJsonDatas = new EntityJsonData[entity.Count];
        for (int i = 0; i < entity.Count; i++)
        {
            entityJsonDatas[i] = new EntityJsonData().Serialize(entity[i]);
        }
        string json = JsonUtility.ToJson(entityJsonDatas);
        System.IO.File.WriteAllText(Application.dataPath + "/SaveData/Entity.json", json);
    }

    public static List<EntityJsonData> LoadEntity()
    {
        string json = System.IO.File.ReadAllText(Application.dataPath + "/SaveData/Entity.json");
        return JsonUtility.FromJson<List<EntityJsonData>>(json);
    }
}
