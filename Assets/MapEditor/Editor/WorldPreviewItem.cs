using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class WorldPreviewItem : VisualElement
{
    private Ground ground;

    public WorldPreviewItem(Ground ground)
    {
        this.ground = ground;

        style.width = 20;
        style.height = 20;
        Sprite sprite = GetSprite();
        
        var img = style.backgroundImage;
        Background background = img.value;
        background.sprite = sprite;
        img.value = background;
        style.backgroundImage = img;
    }

    private Sprite GetSprite()
    {
        TileBase tileBase = ground.groundSO.groundTile;
        if (tileBase is Tile)
        {
            Tile tile = tileBase as Tile;
            return tile.sprite;
        }
        if (tileBase is RuleTile)
        {
            RuleTile ruleTile = tileBase as RuleTile;
            return ruleTile.m_DefaultSprite;
        }
        if (tileBase is AnimatedTile)
        {
            AnimatedTile animatedTile = tileBase as AnimatedTile;
            return animatedTile.m_AnimatedSprites[0];
        }
        return null;
    }
}
