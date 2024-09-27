using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBuildingEvent : BuildingEvent
{
    private Dictionary<Vector2Int, BaseBuilding> buildingDatas;
    public Vector2Int pos;
    public override EnumEventResult ExcuteEvent()
    {
        if (!BuildingManager.Instance.CanBuild(pos, building.buildingSO))
            return EnumEventResult.Failed;


        foreach (KeyValuePair<Vector2Int, TileBase> keyValue in building.buildingSO.tileDatas)
        {
            Vector2Int tilePos = pos + keyValue.Key;
            TileBase tile = keyValue.Value;

            buildingDatas[tilePos] = building;
            BuildingManager.Instance.tilemap.SetTile((Vector3Int)tilePos, tile);
        }

        building.pos = pos;
        return EnumEventResult.Successed;
    }
    public CreateBuildingEvent(BaseBuilding building, Dictionary<Vector2Int, BaseBuilding> buildingDatas, Vector2Int pos) : base(building)
    {
        this.buildingDatas = buildingDatas;
        this.pos = pos;
    }
}
