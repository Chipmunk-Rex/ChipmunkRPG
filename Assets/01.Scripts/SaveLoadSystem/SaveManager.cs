using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : JsonSaver
{
    List<JsonSaver> jsonSavers = new();

    void Awake()
    {
        foreach (var jsonSaver in GetComponentsInChildren<JsonSaver>())
        {
            if (jsonSaver != this)
                jsonSavers.Add(jsonSaver);
        }
    }
    [ContextMenu("Load")]
    public override void Load()
    {
        foreach (var jsonSaver in jsonSavers)
        {
            jsonSaver.Load();
        }
    }
    [ContextMenu("Save")]
    public override void Save()
    {
        foreach (var jsonSaver in jsonSavers)
        {
            jsonSaver.Save();
        }
    }


}
