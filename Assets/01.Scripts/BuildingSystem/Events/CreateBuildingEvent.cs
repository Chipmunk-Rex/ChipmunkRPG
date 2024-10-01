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
            Debug.Log($"설치 {tilePos}");
            TileBase tile = keyValue.Value;

            Ground ground = world.GetGround(tilePos);
            if (ground == null)
            {
                Debug.LogError($"CreateBuildingEvent : ground value is null!!");
                return EnumEventResult.Failed;
            }
            Debug.Log($"{tilePos} {world.GetBuilding(tilePos) == null}");
            ground.building = building;

            world.buildingTilemap.SetTile((Vector3Int)tilePos, tile);
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
