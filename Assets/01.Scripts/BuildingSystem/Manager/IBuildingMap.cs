using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingMap<TBuilding>
{
    public abstract void CreateBuilding(TBuilding building, Vector2Int pos);
    public abstract void RemoveBuilding(Vector2Int pos);
    public abstract TBuilding GetBuilding(Vector2Int pos);
    public abstract List<Vector2Int> GetBuildingPosList(TBuilding building);
    public abstract void CreateBuilding(Vector2Int pos, TBuilding building);
    public abstract bool CanBuild(Vector2Int pos, BuildingSO buildingSO);
}