using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System;
using UnityEditor;

public class ItemResourceFold : VisualElement
{
    Type type;
    public VisualElement foldout { get; private set; }
    public Action<Type> onClick;
    public ItemResourceFold()
    {
        this.style.flexDirection = FlexDirection.Row;
    }
    public void Initialize(Type type)
    {
        this.type = type;
        bool isRoot = TypeCache.GetTypesDerivedFrom(type).Count == 0;

        foldout = new Foldout();
        this.Add(foldout);
        if (foldout is Foldout)
            (foldout as Foldout).text = type.ToString();
        if (type.IsAbstract)
            return;
        Button addButton = new();
        addButton.AddToClassList("ResourceAdd");

        addButton.RegisterCallback<ClickEvent>(evt => OnClick());
        this.Add(addButton);
    }
    private void OnClick()
    {
        onClick?.Invoke(type);
    }

}
