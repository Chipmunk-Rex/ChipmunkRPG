using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(BiomeTable))]
public class BiomeTableEditor : Editor
{
    [SerializeField]
    VisualTreeAsset visualTreeAsset;
    PieChartView pieChartView;
    VisualElement root;
    public override VisualElement CreateInspectorGUI()
    {
        root = new VisualElement();
        visualTreeAsset.CloneTree(root);


        InitializeElement();

        DrawInspector();

        return root;
    }

    private void DrawInspector()
    {
        SerializedProperty property = serializedObject.GetIterator();
        property.NextVisible(true);
        while (property.NextVisible(false))
        {
            PropertyField propertyField = new PropertyField(property);
            propertyField.SetEnabled(property.name != "m_Script");
            root.Add(propertyField);
        }

    }

    private void InitializeElement()
    {
        BiomeTable biomeTable = target as BiomeTable;

        PieChartData[] pieChartDatas = biomeTable.biomeDatas.ToArray();
        pieChartView = new PieChartView(pieChartDatas, 60);
        root.Add(pieChartView);

    }
}
