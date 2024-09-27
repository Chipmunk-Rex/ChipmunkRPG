using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuildingManager<TBuilding, TBuildingManager> : MonoSingleton<TBuildingManager> where TBuildingManager : BaseBuildingManager<TBuilding, TBuildingManager>
{
    protected Dictionary<Vector2Int, TBuilding> buildingDatas = new();
    public abstract void CreateBuilding(Vector2Int pos, TBuilding building);
    public abstract void RemoveBuilding(Vector2Int pos);
    public abstract TBuilding GetBuilding(Vector2Int pos);
    public abstract List<Vector2Int> GetBuildingPosList(TBuilding building);
    public abstract void SetBuilding(Vector2Int pos, TBuilding building);
    public abstract bool CanBuild(Vector2Int pos, BuildingSO buildingSO);
}
