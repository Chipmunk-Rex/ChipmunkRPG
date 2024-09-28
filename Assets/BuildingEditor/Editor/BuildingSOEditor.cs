using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Chipmunk.Library.BuildingEditor
{
    [CustomEditor(typeof(BuildingSO))]
    public class BuildingSOEditor : Editor
    {
        public VisualTreeAsset treeAsset;
        BuildingSO buildingSO;
        InspectorView buildingInspectorView;
        InspectorView selectTileInspectorView;
        ObjectField tileField;
        BuildingTileViewContainer buildingTileViewContainer;

        Vector2Int selectedTilepos;
        // IMGUIContainer
        public override VisualElement CreateInspectorGUI()
        {
            buildingSO = target as BuildingSO;

            if (buildingSO.tileDatas == null)
            {
                Debug.Log("dictionary 생성");
                buildingSO.tileDatas = new();
            }

            VisualElement root = new VisualElement();
            treeAsset.CloneTree(root);

            QuearyElements(root);
            InitailizeElement();

            return root;
        }

        private void QuearyElements(VisualElement root)
        {
            buildingTileViewContainer = root.Q<BuildingTileViewContainer>();
            buildingInspectorView = root.Q<InspectorView>("BuildingInspector");
            selectTileInspectorView = root.Q<InspectorView>("TileInspector");
            tileField = root.Q<ObjectField>("TileField");

            #region buttonRegion
            VisualElement topBtn = root.Q("TopButton");
            topBtn.RegisterCallback<ClickEvent>(evt =>
            {
                buildingSO.top++;
                buildingTileViewContainer.LoadView(buildingSO);
            });
            VisualElement rightBtn = root.Q("RightButton");
            rightBtn.RegisterCallback<ClickEvent>(evt =>
            {
                buildingSO.right++;
                buildingTileViewContainer.LoadView(buildingSO);
            });
            VisualElement bottomBtn = root.Q("BottomButton");
            bottomBtn.RegisterCallback<ClickEvent>(evt =>
            {
                buildingSO.down++;
                buildingTileViewContainer.LoadView(buildingSO);
            });
            VisualElement leftBtn = root.Q("LeftButton");
            leftBtn.RegisterCallback<ClickEvent>(evt =>
            {
                buildingSO.left++;
                buildingTileViewContainer.LoadView(buildingSO);
            });
            #endregion
        }
        private void InitailizeElement()
        {
            buildingInspectorView.DrawInspector(buildingSO);
            buildingInspectorView.onTrackValue += OnBuildingInspectorValueChange;
            buildingTileViewContainer.LoadView(buildingSO);
            buildingTileViewContainer.onClickTile += OnSelectTileView;
            tileField.RegisterValueChangedCallback(OnTileFieldValueChanged);
            tileField.style.visibility = Visibility.Hidden;
        }

        private void OnTileFieldValueChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            TileBase tile = evt.newValue as TileBase;

            if (tile == null)
                buildingSO.tileDatas.Remove(selectedTilepos);
            else
                buildingSO.tileDatas[selectedTilepos] = tile;

            OnSelectTileView(selectedTilepos);
        }

        private void OnSelectTileView(Vector2Int pos)
        {
            tileField.style.visibility = Visibility.Visible;
            selectedTilepos = pos;

            if (!buildingSO.tileDatas.ContainsKey(pos))
            {
                tileField.value = null;
                selectTileInspectorView.HideInspector();
                return;
            }

            TileBase tile = buildingSO.tileDatas[pos];

            tileField.value = tile;
            selectTileInspectorView.DrawInspector(tile);
        }

        private void OnBuildingInspectorValueChange(SerializedObject @object)
        {
            buildingTileViewContainer.LoadView(buildingSO);
        }

        public void InspectorView()
        {
            buildingInspectorView.DrawInspector(buildingSO);
        }
        public void SelectTileInspectorView(TileBase tile)
        {
            selectTileInspectorView.DrawInspector(tile);
        }
    }
}