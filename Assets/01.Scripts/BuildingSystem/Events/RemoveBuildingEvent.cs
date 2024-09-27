using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RemoveBuildingEvent : BuildingEvent
{
    private Dictionary<Vector2Int, BaseBuilding> buildingDatas;
    private Vector2Int pos;
    public RemoveBuildingEvent(BaseBuilding building, Dictionary<Vector2Int, BaseBuilding> buildingDatas, Vector2Int pos) : base(building)
    {
        this.buildingDatas = buildingDatas;
        this.pos = pos;
    }

    public override EnumEventResult ExcuteEvent()
    {
        foreach (Vector2Int localPos in building.buildingSO.tileDatas.Keys)
        {
            Vector2Int tilePos = pos + localPos;

            buildingDatas[tilePos] = null;
            BuildingManager.Instance.tilemap.SetTile((Vector3Int)tilePos, null);
        }

        return EnumEventResult.Successed;
    }
}
