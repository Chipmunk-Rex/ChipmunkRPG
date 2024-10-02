using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RemoveBuildingEvent : BuildingEvent
{
    private Vector2Int pos;
    public RemoveBuildingEvent(World world, BaseBuilding building) : base(world, building)
    {
        this.pos = building.pos;
    }

    public override EnumEventResult ExcuteEvent()
    {
        foreach (Vector2Int localPos in building.buildingSO.tileDatas.Keys)
        {
            Vector2Int worldTilePos = pos + localPos;

            Ground ground = world.GetGround(worldTilePos);
            ground.building.currentWorld = null;
            ground.building = null;

            world.buildingTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldTilePos), null);
        }

        return EnumEventResult.Successed;
    }
}
