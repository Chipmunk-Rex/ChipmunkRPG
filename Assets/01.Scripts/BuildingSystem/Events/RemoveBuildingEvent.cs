using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RemoveBuildingEvent : BuildingEvent
{
    private Vector2Int pos;
    public RemoveBuildingEvent(World world, Building building) : base(world, building)
    {
        this.pos = building.pos;
    }

    public override EnumEventResult ExcuteEvent()
    {
        foreach (Vector2Int localPos in building.buildingSO.tileDatas.Keys)
        {
            Vector2Int worldTilePos = pos + localPos;

            Ground ground = world.GetGround(worldTilePos);
            ground.building = null;
            // ground.building.currentWorld = null;


            if (!building.buildingSO.islower)
                world.buildingTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldTilePos), null);
            else
                world.lowerBuildingTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldTilePos), null);
        }
        building?.buildingEntity?.Die();

        return EnumEventResult.Successed;
    }
}
