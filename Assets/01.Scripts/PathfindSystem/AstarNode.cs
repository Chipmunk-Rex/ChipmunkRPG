
using UnityEngine;

public class AstarNode
{
    public AstarNode parentNode;
    public Vector2Int position;
    public int F
    {
        get { return G + H; }
    }
    public int G;
    public int H;
    public AstarNode(Vector2Int position)
    {
        this.position = position;
    }
}