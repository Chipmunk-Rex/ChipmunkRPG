using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPer : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] Color[] posibleColor;
    PerlinNoise perlinNoise;
    [SerializeField] float scale = 10;
    [SerializeField] int seed;
    private void Awake()
    {
        perlinNoise = new(scale, seed);
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
                float value = perlinNoise.CalculateNoise(new Vector2(x, y) / 2);;
                int colorIndex = Mathf.RoundToInt((value / (1 / (float)posibleColor.Length)) * 10) / 10;
                Color color = posibleColor[colorIndex];
                texture.SetPixel(x, y, color);
            }

        texture.Apply();
        rawImage.texture = texture;
    }
}
