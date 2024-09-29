using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    public EnumBiomType[,] biomTypes;
    public Tilemap tilemap;
    public Vector2Int position;
    private Vector2Int chunkSize;
    public void Initialize(Vector2Int position, Vector2Int chunkSize)
    {
        this.position = position;
        this.transform.position = (Vector2)position + new Vector2(chunkSize.x / 2, -chunkSize.y / 2);
        this.chunkSize = chunkSize;

        biomTypes = new EnumBiomType[chunkSize.x, chunkSize.y];
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector2)chunkSize);
    }
#endif
}
