using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerJsonSaver : JsonSaver
{
    [field: SerializeField] public Player player { get; private set; }
    [ContextMenu("JsonLoad")]
    public override void Load()
    {
        string json = System.IO.File.ReadAllText(Application.dataPath + "/SaveData/Player.json");
        PlayerJsonData data = JsonConvert.DeserializeObject<PlayerJsonData>(json,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }
        );
        player.Initialize(data);
    }

    [ContextMenu("JsonSave")]
    public override void Save()
    {
        PlayerJsonData data = new PlayerJsonData().Serialize(player);

        string json = JsonConvert.SerializeObject(data,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }
        );

        System.IO.Directory.CreateDirectory(Application.dataPath + "/SaveData");
        System.IO.File.WriteAllText(Application.dataPath + "/SaveData/Player.json", json);
    }
}
