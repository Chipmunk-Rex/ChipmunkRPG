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
        Debug.Log("CreateBuildingEvent :" + world.CanBuild(pos, building.buildingSO));
        if (!world.CanBuild(pos, building.buildingSO))
            return EnumEventResult.Failed;
        Debug.Log("CreateBuildingEvent : Can Build");
        for (int x = -building.buildingSO.left; x <= building.buildingSO.right; x++)
        {
            for (int y = -building.buildingSO.down; y <= building.buildingSO.top; y++)
            {
                Vector2Int tilePos = new Vector2Int(x, y);
                if (tilePos != Vector2Int.zero && building.buildingSO.tileDatas.ContainsKey(tilePos) == false)
                    continue;

                Vector2Int groundWorldPos = pos + tilePos;

                Ground ground = world.GetGround(groundWorldPos);
                if (ground == null)
                {
                    Debug.LogError($"CreateBuildingEvent : ground value is null!!");
                    return EnumEventResult.Failed;
                }
                if (ground.building != null)
                {
                    Debug.LogError($"CreateBuildingEvent : building is already exist!! {pos}");
                    return EnumEventResult.Failed;
                }

                ground.building = building;

                building.pos = pos;

                if (building.buildingSO.tileDatas.ContainsKey(tilePos))
                {
                    TileBase tile = building.buildingSO.tileDatas[tilePos];
                    if (!building.buildingSO.islower)
                        world.buildingTilemap.SetTile(Vector3Int.RoundToInt((Vector2)groundWorldPos), tile);
                    else
                        world.lowerBuildingTilemap.SetTile(Vector3Int.RoundToInt((Vector2)groundWorldPos), tile);
                }
            }
        }

        if (building.buildingEntity != null)
            building.buildingEntity.SpawnEntity(pos: pos);

        building.pos = pos;
        return EnumEventResult.Successed;
    }
    public CreateBuildingEvent(World world, Building building, Vector2Int pos) : base(world, building)
    {
        this.pos = pos;
    }
}
