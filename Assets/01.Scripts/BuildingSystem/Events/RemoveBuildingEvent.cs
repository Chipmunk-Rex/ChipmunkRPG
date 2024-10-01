using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RemoveBuildingEvent : BuildingEvent
{
    private World world;
    private Vector2Int pos;
    public RemoveBuildingEvent(BaseBuilding building, World world) : base(building)
    {
        this.world = world;
        this.pos = building.pos;
    }

    public override EnumEventResult ExcuteEvent()
    {
        foreach (Vector2Int localPos in building.buildingSO.tileDatas.Keys)
        {
            Vector2Int worldTilePos = pos + localPos;

            Ground ground = world.GetGround(worldTilePos);
            ground.baseBuilding = null;

            world.buildingTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldTilePos), null);
        }

        return EnumEventResult.Successed;
    }
}
