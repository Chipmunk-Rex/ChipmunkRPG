using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSprite : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] Color[] posibleColor;
    VoronoiNoise voronoiNoise;
    [SerializeField] int cellSize = 3;
    [SerializeField] int seed;
    private void Awake()
    {
        voronoiNoise = new(cellSize, seed);
        Genarate();
    }

    private void Genarate()
    {
        RectTransform rect = rawImage.GetComponent<RectTransform>();
        int size = Mathf.RoundToInt(rect.sizeDelta.x);
        Texture2D texture = new Texture2D(size, size);
        texture.filterMode = FilterMode.Point;

        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
            {
                int value = voronoiNoise.CalculateNoise(new Vector2Int(x, y));
                int colorIndex = value / (int.MaxValue / posibleColor.Length);
                Color color = posibleColor[colorIndex];
                if(voronoiNoise.isCell(new Vector2Int(x, y)))
                    color = Color.black;
                texture.SetPixel(x, y, color);
            }

        texture.Apply();
        rawImage.texture = texture;
    }
}
