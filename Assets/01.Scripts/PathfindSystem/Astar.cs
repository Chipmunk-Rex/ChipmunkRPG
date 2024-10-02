using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public static class Astar
{
    public static List<Vector2Int> FindPath(Vector2 firstPos, Vector2 targetPos, World world)
    {
        Debug.Log(targetPos);
        AstarNode startNode = new AstarNode(Vector2Int.RoundToInt(firstPos));
        AstarNode endNode = new AstarNode(Vector2Int.RoundToInt(targetPos));
        AstarNode currentNode;
        List<Vector2Int> path = new();
        List<AstarNode> openList = new();
        List<AstarNode> closeList = new();

        openList.Add(startNode);
        currentNode = startNode;
        int x = 0;
        while (openList.Count > 0)
        {//마지막 위치가 도착지점이 될때까지 실행
            currentNode = openList[0];
            foreach (AstarNode node in openList)
            {
                if (currentNode.F > node.F)
                    currentNode = node;
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, 1), endNode, ref openList, closeList, world);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, -1), endNode, ref openList, closeList, world);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(1, 0), endNode, ref openList, closeList, world);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(-1, 0), endNode, ref openList, closeList, world);

            if (currentNode.position == endNode.position) break;

            x++;
            if (x > 300)
            {
                Debug.LogError("Astar : 오류 발생 (300회 이상 반복됨) ");
                return null;
            }
        }

        AstarNode endCurNode = currentNode;
        while (endCurNode.parentNode != null)
        {
            path.Add(endCurNode.position);
            endCurNode = endCurNode.parentNode;
        }
        path.Add(startNode.position);
        path.Reverse();
        Debug.Log(path.Count);
        return path;
    }

    static ref List<AstarNode> AddOpenList(AstarNode currentNode, Vector2Int checkPos, AstarNode endNode, ref List<AstarNode> openList, List<AstarNode> closeList, World world)
    {
        AstarNode newNode = new AstarNode(checkPos);
        if (CheckCanMove(checkPos, world))
            // if (Physics2D.Raycast(checkPos, Vector2.zero, 0.1f).collider == null)
            if (closeList.Find(node => node.position == checkPos) == null && openList.Find(node => node.position == checkPos) == null)
            {
                int G = currentNode.G + 1;
                int H = Mathf.Abs(endNode.position.x - checkPos.x) + Mathf.Abs(endNode.position.y - checkPos.y);
                newNode.G = G;
                newNode.H = H;
                newNode.parentNode = currentNode;
                openList.Add(newNode);
            }
        return ref openList;
    }
    static bool CheckCanMove(Vector2Int checkPos, World world)
    {
        Ground ground = world.GetGround(checkPos);
        if (ground.groundSO.groundType != EnumGroundType.Ground)
            return false;

        return true;
    }
    static bool CheckPriority(AstarNode currentPos, AstarNode checkPos)
    {
        Debug.Log($"G : {checkPos.G} H : {checkPos.H} currentPos.G : {currentPos.G} currentPos.H : {currentPos.H}");
        Debug.Log("bool " + (currentPos.F >= checkPos.F && currentPos.H < checkPos.H));
        if (currentPos.G + currentPos.H >= checkPos.G + checkPos.H && currentPos.H > checkPos.H)
            return true;
        else
            return false;
    }
}