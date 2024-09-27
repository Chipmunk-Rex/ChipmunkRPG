using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : BaseBuildingManager<BaseBuilding, BuildingManager>
{
    public EventMediatorContainer<EnumBuildingEvent> eventContainer = new();
    public Tilemap tilemap;
    public override bool CanBuild(Vector2Int worldPos, BuildingSO buildingSO)
    {
        foreach (Vector2Int localPos in buildingSO.tileDatas.Keys)
        {
            Vector2Int tilePos = worldPos + localPos;
            if (GetBuilding(tilePos) != null)
            {
                Debug.Log("실행");
                return false;
            }
        }
        return true;
    }

    public override void CreateBuilding(Vector2Int pos, BaseBuilding building)
    {
        CreateBuildingEvent @event = new CreateBuildingEvent(buildingDatas, building, pos);

        eventContainer.Execute(EnumBuildingEvent.CreateBuilding, @event);
    }

    public override BaseBuilding GetBuilding(Vector2Int pos)
    {
        if (!buildingDatas.ContainsKey(pos))
            buildingDatas.Add(pos, null);

        return buildingDatas[pos];
    }

    public override List<Vector2Int> GetBuildingPosList(BaseBuilding building)
    {
        List<Vector2Int> buildingPosList = new();
        foreach (Vector2Int localPos in building.buildingSO.tileDatas.Keys)
        {
            Vector2Int tilePos = building.pos + localPos;
            buildingPosList.Add(tilePos);
        }
        return buildingPosList;
    }

    public override void RemoveBuilding(Vector2Int pos)
    {
        BaseBuilding building = GetBuilding(pos);
        List<Vector2Int> buildingPosList = GetBuildingPosList(building);

        // 추가 구현 필요
    }

    public override void SetBuilding(Vector2Int pos, BaseBuilding building)
    {
        throw new System.NotImplementedException();
    }
}
