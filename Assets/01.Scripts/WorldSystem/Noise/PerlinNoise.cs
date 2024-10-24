using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{
    private float offset;
    private int seed;
    private float scale;
    public PerlinNoise(float scale, int seed)
    {
        this.seed = seed;
        this.scale = scale;

        Random.InitState(seed);
        offset = Random.Range(-100, 100);
    }
    public float CalculateNoise(Vector2 position)
    {
        float xCoord = position.x / scale + offset;
        float yCoord = position.y / scale + offset;

        return Mathf.Clamp(Mathf.PerlinNoise(xCoord, yCoord), 0, 1 - Mathf.Epsilon);
    }
}
