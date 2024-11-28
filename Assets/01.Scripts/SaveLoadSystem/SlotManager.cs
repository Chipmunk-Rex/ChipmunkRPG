using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoSingleton<SlotManager>
{
    public string currentSlotPath;

    public NDSData GetWorldNDSData()
    {
        Debug.Log($"{currentSlotPath}/world.json");
        string json = System.IO.File.ReadAllText($"{currentSlotPath}/world.json");
        NDSData ndsData = Newtonsoft.Json.JsonConvert.DeserializeObject<NDSData>(json);
        return ndsData;
    }
    internal SlotData GetSlotData()
    {
        string json = System.IO.File.ReadAllText($"{currentSlotPath}/slotData.json");
        SlotData slotData = Newtonsoft.Json.JsonConvert.DeserializeObject<SlotData>(json);
        return slotData;
    }
}
