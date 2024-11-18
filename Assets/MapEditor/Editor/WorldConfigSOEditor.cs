using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(WorldConfigSO))]
public class WorldConfigSOEditor : Editor
{
    WorldConfigSO worldConfigSO;

    #region View
    PieChartView pieChartView;
    WorldPreview worldPreview;
    #endregion
    public override VisualElement CreateInspectorGUI()
    {
        worldConfigSO = target as WorldConfigSO;

        VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/MapEditor/Editor/WorldConfigSOEditor.uxml");
        VisualElement root = visualTreeAsset.CloneTree();

        Button refreshBtn = new Button(Refresh);
        refreshBtn.text = "Refresh";
        refreshBtn.style.width = 100;
        root.Add(refreshBtn);

        pieChartView = new PieChartView(worldConfigSO.biomeDatas.ToArray(), 50);
        pieChartView.onDataChanged += OnChartDataChanged;
        pieChartView.onSelect += OnSelectPoint;
        root.Add(pieChartView);

        worldPreview = new WorldPreview();
        root.Add(worldPreview);

        SerializedProperty property = serializedObject.GetIterator();
        property.NextVisible(true);
        while (property.NextVisible(false))
        {
            PropertyField propertyField = new PropertyField(property);
            propertyField.SetEnabled(property.name != "m_Script");
            root.Add(propertyField);
        }

        return root;
    }

    private void Refresh()
    {
        worldConfigSO.biomeDatas.Sort((a, b) => a.percentage.CompareTo(b.percentage));
        worldConfigSO.biomeDatas[worldConfigSO.biomeDatas.Count - 1].percentage = 100;

        pieChartView.DrawView(worldConfigSO.biomeDatas.ToArray());
        worldPreview.DrawView(worldConfigSO);
    }

    private void OnChartDataChanged(PieChartData data)
    {
        // PieChartData<WorldBiomeData> pieChartData = data as PieChartData<WorldBiomeData>;
        // pieChartDataView.DrawView(data);
    }

    private void OnSelectPoint(PieChartData data)
    {
        Debug.Log("OnSelectPoint");
        PieChartData<BiomeSO> pieChartData = data as PieChartData<BiomeSO>;
        Debug.Log(pieChartData);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        EditorUtility.SetDirty(worldConfigSO);
    }
}
