using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBuildingEvent : BuildingEvent
{
    private Dictionary<Vector2Int, Ground> groundDatas;
    private World world;
    public Vector2Int pos;
    public override EnumEventResult ExcuteEvent()
    {
        if (!world.CanBuild(pos, building.buildingSO))
            return EnumEventResult.Failed;


        foreach (KeyValuePair<Vector2Int, TileBase> keyValue in building.buildingSO.tileDatas)
        {
            Vector2Int tilePos = pos + keyValue.Key;
            TileBase tile = keyValue.Value;

            // Ground ground =
            // world..SetTile((Vector3Int)tilePos, tile);
        }

        building.pos = pos;
        return EnumEventResult.Successed;
    }
    public CreateBuildingEvent(BaseBuilding building, World world, Vector2Int pos) : base(building)
    {
        this.world = world;
        this.pos = pos;
    }
}
