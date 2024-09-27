using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BuildingTileView : VisualElement
{
    Vector2Int pos;
    public Action<Vector2Int> onClick;

    public BuildingTileView(Vector2Int pos)
    {
        this.AddToClassList("BuildingTileView");
        this.pos = pos;
        this.RegisterCallback<ClickEvent>(evt => onClick?.Invoke(pos));
    }
}
