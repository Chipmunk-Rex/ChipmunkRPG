using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldPreview : VisualElement
{
    VoronoiNoise voronoiNoise;
    PerlinNoise perlinNoise;
    WorldConfigSO worldSO;
    #region elements 
    Vector2IntField preViewSizeField;
    IntegerField seedField;
    #endregion
    public WorldPreview()
    {
        Initialize(new Vector2Int(10, 10));
    }
    public void Initialize(Vector2Int previewSize, int seed = int.MaxValue)
    {
        preViewSizeField = new Vector2IntField("Preview Size");
        preViewSizeField.value = previewSize;

        seedField = new IntegerField("PreviewSeed");
        seedField.value = seed;

        this.Add(preViewSizeField);
        this.Add(seedField);
    }
    public void DrawView(WorldConfigSO worldSO)
    {
        DrawView(worldSO, seedField.value, preViewSizeField.value);
    }
    public void DrawView(WorldConfigSO worldSO, int seed, Vector2Int previewSize)
    {
        this.Clear();
        Initialize(previewSize, seed);

        this.worldSO = worldSO;

        voronoiNoise = new VoronoiNoise(worldSO.biomSize, seed, worldSO.biomDetail);
        perlinNoise = new PerlinNoise(worldSO.depthScale, seed);

        int width = previewSize.x;
        int height = previewSize.y;
        for (int y = -(height / 2); y < height / 2; y++)
        {
            VisualElement row = new VisualElement();
            row.name = "row";
            row.style.flexDirection = FlexDirection.Row;

            for (int x = -(width / 2); x < width / 2; x++)
            {
                Vector2Int worldPos = new Vector2Int(x, y);
                Ground ground = new GroundBuilder()
                    .Position(worldPos)
                    .VoronoiNoise(voronoiNoise)
                    .PerlinNoise(perlinNoise)
                    .World(worldSO)
                    .Build();

                WorldPreviewItem worldPreviewItem = new WorldPreviewItem(ground);

                row.Add(worldPreviewItem);
            }
            this.Add(row);
        }
    }
}
