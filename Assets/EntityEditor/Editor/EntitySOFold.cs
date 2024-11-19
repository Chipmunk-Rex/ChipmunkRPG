using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EntitySOFold : VisualElement
{
    Type type;
    public VisualElement element { get; private set; }
    public EntitySOFold()
    {
    }
    public void DrawView(Type type)
    {


        this.type = type;
        DrawElement(type);
        DrawButton(type);
    }

    private void DrawButton(Type type)
    {
        if (type.IsAbstract)
            return;

        Button addButton = new();
        addButton.text = "Create";
        addButton.AddToClassList("ResourceAdd");

        addButton.RegisterCallback<ClickEvent>(OnClick);
        this.Add(addButton);
    }

    private void DrawElement(Type type)
    {
        bool isRoot = TypeCache.GetTypesDerivedFrom(type).Count == 0;
        if (isRoot)
        {
            Label label = new Label();
            label.text = $"     {type.ToString()}";
            element = label;
        }
        else
        {
            Foldout foldout = new Foldout();
            foldout.text = $"{type.ToString()}";
            element = foldout;
        }
        element.AddToClassList("ResourceView");
        this.Add(element);
    }
    private void OnClick(ClickEvent evt)
    {
        EntitySO scriptableObject = ScriptableObject.CreateInstance(type) as EntitySO;

        Undo.RegisterCreatedObjectUndo(scriptableObject, "ItemEditor Create Item");

        SaveScriptableObject(scriptableObject, $"Assets/EntityEditor/ScriptableObject/{type.ToString()}");
    }

    private void SaveScriptableObject(EntitySO scriptableObject, string path)
    {
        EntitySO finedSO = AssetDatabase.LoadAssetAtPath<EntitySO>($"{path}.asset");
        int count = 0;
        if (finedSO == null)
        {
            AssetDatabase.CreateAsset(scriptableObject, $"{path}.asset");
            AssetDatabase.SaveAssets();
            return;
        }
        else
        {
            while (finedSO != null)
            {
                count++;
                finedSO = AssetDatabase.LoadAssetAtPath<EntitySO>($"{path} {count}.asset");
            }
        }
        AssetDatabase.CreateAsset(scriptableObject, $"{path} {count}.asset");
        AssetDatabase.SaveAssets();
    }
}
