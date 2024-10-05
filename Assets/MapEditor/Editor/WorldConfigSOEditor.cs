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
    public override VisualElement CreateInspectorGUI()
    {
        worldConfigSO = target as WorldConfigSO;

        VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/MapEditor/Editor/WorldConfigSOEditor.uxml");
        VisualElement root = visualTreeAsset.CloneTree();

        root.Add(new PieChartView(worldConfigSO.biomes.ToArray()));

        root.Q<Label>().AddManipulator(new DragAndDropManipulator(root.Q<Label>()));

        return root;
    }
}
