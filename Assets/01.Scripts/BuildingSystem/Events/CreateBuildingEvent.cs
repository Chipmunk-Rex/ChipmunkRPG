using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBuildingEvent : BuildingEvent
{
    private Dictionary<Vector2Int, Ground> groundDatas;
    public Vector2Int pos;
    public override EnumEventResult ExcuteEvent()
    {
        if (!world.CanBuild(pos, building.buildingSO))
            return EnumEventResult.Failed;

        for (int x = -building.buildingSO.left; x <= building.buildingSO.right; x++)
        {
            for (int y = -building.buildingSO.down; y <= building.buildingSO.top; y++)
            {
                Vector2Int tilePos = new Vector2Int(x, y);
                if (building.buildingSO.tileDatas.ContainsKey(tilePos) == false)
                    continue;

                Vector2Int tileWorldPos = pos + tilePos;
                Debug.Log($"설치 {tileWorldPos}");
                TileBase tile = building.buildingSO.tileDatas[tilePos];

                Ground ground = world.GetGround(tileWorldPos);
                if (ground == null)
                {
                    Debug.LogError($"CreateBuildingEvent : ground value is null!!");
                    return EnumEventResult.Failed;
                }
                Debug.Log($"{tileWorldPos} {world.GetBuilding(tileWorldPos) == null}");
                ground.building = building;

                world.buildingTilemap.SetTile((Vector3Int)tileWorldPos, tile);
            }
        }
        // foreach (KeyValuePair<Vector2Int, TileBase> keyValue in building.buildingSO.tileDatas)
        // {
        //     Vector2Int tilePos = pos + keyValue.Key;
        //     Debug.Log($"설치 {tilePos}");
        //     TileBase tile = keyValue.Value;

        //     Ground ground = world.GetGround(tilePos);
        //     if (ground == null)
        //     {
        //         Debug.LogError($"CreateBuildingEvent : ground value is null!!");
        //         return EnumEventResult.Failed;
        //     }
        //     Debug.Log($"{tilePos} {world.GetBuilding(tilePos) == null}");
        //     ground.building = building;

        //     world.buildingTilemap.SetTile((Vector3Int)tilePos, tile);
        // }

        building.pos = pos;
        building.currentWorld = world;

        return EnumEventResult.Successed;
    }
    public CreateBuildingEvent(World world, BaseBuilding building, Vector2Int pos) : base(world, building)
    {
        this.pos = pos;
    }
}
