using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// [CustomEditor(typeof(BiomeTable))]
public class BiomeTableSOEditor : Editor
{
    [SerializeField]
    VisualTreeAsset visualTreeAsset;
    PieChartView pieChartView;
    VisualElement root;
    BiomeTable biomeTable;
    
    public override VisualElement CreateInspectorGUI()
    {
        biomeTable = target as BiomeTable;
        root = new VisualElement();
        visualTreeAsset.CloneTree(root);

        pieChartView = new PieChartView(biomeTable.biomeDatas.ToArray(), 60);

        return root;
    }
}
