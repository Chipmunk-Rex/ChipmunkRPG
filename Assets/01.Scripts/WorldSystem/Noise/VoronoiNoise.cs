using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiNoise
{
    /// <summary>
    /// (셀 좌표, 좌표)
    /// </summary>
    Dictionary<Vector2Int, (Vector2, int)> noiseCellData = new();
    int cellSize;
    readonly int seed;
    public VoronoiNoise(int cellSize, int seed)
    {
        this.cellSize = cellSize;
        this.seed = seed;
    }
    private void GenarateNoise(Vector2Int cellPos)
    {
        if (!noiseCellData.ContainsKey(cellPos))
        {
            Random.InitState(cellPos.x * 1000 + cellPos.y);
            int posSeed = Random.Range(0, int.MaxValue);

            Random.InitState(posSeed + seed);
            int noiseValue = Random.Range(0, int.MaxValue);

            // Vector2 cellPosition = Vector2.zero;
            // Vector2 cellPosition = new Vector2(
            // Random.Range(cellPos.x * cellSize, (cellPos.x + 1) * cellSize),
            // Random.Range(cellPos.y * cellSize, (cellPos.y + 1) * cellSize)
            // );
            Vector2 cellPosition = new Vector2(Random.Range(0, (float)cellSize), Random.Range(0, (float)cellSize));

            noiseCellData.Add(cellPos, (cellPosition, noiseValue));
        }
    }
    public int CalculateNoise(Vector2Int position)
    {
        Vector2Int cellPos = position / cellSize;
        if (!noiseCellData.ContainsKey(cellPos))
            GenarateNoise(cellPos);

        Vector2Int[] cellPosArray = new Vector2Int[] {
            cellPos + new Vector2Int(-1, 1),
            cellPos + new Vector2Int(0, 1),
            cellPos + new Vector2Int(1, 1),
            cellPos + new Vector2Int(-1, 0),
            cellPos,
            cellPos + new Vector2Int(1, 0),
            cellPos + new Vector2Int(-1, -1),
            cellPos + new Vector2Int(0, -1),
            cellPos + new Vector2Int(1, -1)
        };

        float minDistance = float.MaxValue;
        Vector2Int nearCellPos = Vector2Int.zero;

        foreach (Vector2Int cPos in cellPosArray)
        {
            if (!noiseCellData.ContainsKey(cPos))
                GenarateNoise(cPos);

            Vector2 cellPosData = noiseCellData[cPos].Item1 + cPos * cellSize;
            float distance = Vector2.Distance(position, cellPosData);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearCellPos = cPos;
            }
        }

        return noiseCellData[nearCellPos].Item2;
    }
    public bool isCell(Vector2Int position)
    {
        Vector2Int cellPos = (position / cellSize);
        Vector2 cellPosData = noiseCellData[cellPos].Item1 + cellPos * cellSize;

        if (Vector2Int.RoundToInt(cellPosData) == position)
            return true;
        return false;
    }
}
