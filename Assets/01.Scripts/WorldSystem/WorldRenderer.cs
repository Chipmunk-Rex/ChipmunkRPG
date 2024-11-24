using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(World))]
public class WorldRenderer : MonoBehaviour
{
    [SerializeField] private Transform renderTarget;
    [SerializeField] private World world;
    void Awake()
    {
        world = transform.GetComponent<World>();
        StartRenderWorld();
    }

    public void StartRenderWorld()
    {
        StartCoroutine(RenderWorld());
    }

    private IEnumerator RenderWorld()
    {
        while (true)
        {
            Vector2Int targetPos = Vector2Int.RoundToInt(renderTarget.position);
            Vector2Int chunkSize = world.worldSO.chunkSize - Vector2Int.one;
            Vector2Int centerPos = Vector2Int.zero;
            try { centerPos = new Vector2Int((targetPos.x / chunkSize.x) * chunkSize.x, (targetPos.y / chunkSize.y) * chunkSize.y); }
            catch { }
            // Vector2Int centerPos = Vector2Int.RoundToInt(renderTarget.position);
            for (int x = -world.worldSO.renderSize; x < world.worldSO.renderSize; x++)
            {
                for (int y = -world.worldSO.renderSize; y < world.worldSO.renderSize; y++)
                {
                    RenderWorld(centerPos + new Vector2Int(x * chunkSize.x, y * chunkSize.y));
                }
            }
            yield return new WaitForSeconds(world.worldSO.renderDuration);
        }
    }

    private void RenderWorld(Vector2Int centerPos)
    {
        if (world.GetGround(centerPos) != null)
            return;

        Vector2Int minPos = centerPos - world.worldSO.chunkSize / 2;
        Vector2Int maxPos = centerPos + world.worldSO.chunkSize / 2;
        world.GenerateGround(minPos, maxPos);
    }
}
