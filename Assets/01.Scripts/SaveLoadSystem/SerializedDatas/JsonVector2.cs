using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct JsonVector2
{
    public float x;
    public float y;
    public JsonVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public JsonVector2(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }
    public JsonVector2(Vector2Int vector)
    {
        x = vector.x;
        y = vector.y;
    }
    public JsonVector2(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
    }
    public static implicit operator Vector2Int(JsonVector2 jsonVector)
    {
        return new Vector2Int(Mathf.RoundToInt(jsonVector.x), Mathf.RoundToInt(jsonVector.y));
    }
    public static implicit operator Vector2(JsonVector2 jsonVector)
    {
        return new Vector2(jsonVector.x, jsonVector.y);
    }
    public static implicit operator Vector3(JsonVector2 jsonVector)
    {
        return new Vector3(jsonVector.x, jsonVector.y);
    }
    public static implicit operator JsonVector2(Vector2 vector)
    {
        return new JsonVector2 { x = vector.x, y = vector.y };
    }
    public static implicit operator JsonVector2(Vector2Int vector)
    {
        return new JsonVector2 { x = vector.x, y = vector.y };
    }
    public static implicit operator JsonVector2(Vector3 vector)
    {
        return new JsonVector2 { x = vector.x, y = vector.y };
    }
}
