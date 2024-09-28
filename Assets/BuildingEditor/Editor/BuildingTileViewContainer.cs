using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Chipmunk.Library.BuildingEditor
{
    public class BuildingTileViewContainer : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BuildingTileViewContainer, VisualElement.UxmlTraits> { }
        public Action<Vector2Int> onClickTile;
        public BuildingTileView buildingTileView;
        public void LoadView(BuildingSO buildingSO)
        {
            this.Clear();

            Dictionary<Vector2Int, TileBase> tileDatas = buildingSO.tileDatas;
            (int, int, int, int) size = buildingSO.tileDataSize;
            for (int y = size.Item1; y >= -size.Item3; y--)
            {
                VisualElement horizontalContainer = new VisualElement();
                horizontalContainer.AddToClassList("HorizontalContainer");
                for (int x = -size.Item4; x < size.Item2; x++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    BuildingTileView tileView = new BuildingTileView(pos);
                    SetTileView(tileDatas, pos, tileView);

                    horizontalContainer.Add(tileView);
                }
                this.Add(horizontalContainer);
            }
        }

        private void SetTileView(Dictionary<Vector2Int, TileBase> tileDatas, Vector2Int pos, BuildingTileView tileView)
        {
            tileView.onClick += OnTileViewClick;

            if (pos == Vector2Int.zero)
                tileView.AddToClassList("CenterTile");
            if (tileDatas.ContainsKey(pos) && tileDatas[pos] != null)
            {
                tileView.AddToClassList("PlaceTile");

                TileBase tileBase = tileDatas[pos];
                SetTileViewSprite(tileBase, tileView);
            }
        }

        private void SetTileViewSprite(TileBase tileBase, BuildingTileView tileView)
        {
            Sprite sprite = null;
            if (tileBase is Tile)
            {
                sprite = (tileBase as Tile).sprite;
            }
            else
            if (tileBase is RuleTile)
            {
                sprite = (tileBase as RuleTile).m_DefaultSprite;
            }
            else
            if (tileBase is AnimatedTile)
            {
                AnimatedTile animatedTile = (tileBase as AnimatedTile);
                if (animatedTile.m_AnimatedSprites.Length != 0)
                    sprite = animatedTile.m_AnimatedSprites[0];
            }
            if (sprite != null)
            {
                var img = tileView.style.backgroundImage;
                Background background = img.value;
                background.sprite = sprite;
                img.value = background;
                tileView.style.backgroundImage = img;
            }
        }

        private void OnTileViewClick(BuildingTileView view)
        {
            string className = "OnSelect";

            if (buildingTileView != null)
                buildingTileView.RemoveFromClassList(className);
            buildingTileView = view;
            view.AddToClassList(className);
            onClickTile?.Invoke(view.pos);
        }
    }
}