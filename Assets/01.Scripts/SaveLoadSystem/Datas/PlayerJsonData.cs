using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJsonData : EntityJsonData
{
    public string worldName;
    public PlayerJsonData Serialize(Player player)
    {
        worldName = player.currentWorld.worldSO.worldName;
        Serialize(player as Entity);
        return this;
    }
}
