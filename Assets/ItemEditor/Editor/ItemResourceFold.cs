using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemResourceFold : Foldout
{
    public new class UxmlFactory : UxmlFactory<ItemResourceFold, Foldout.UxmlTraits> { }
    public ItemResourceFold()
    {
        VisualElement visualElement = new();
        visualElement.AddToClassList("ResourceAdd");
        this.Add(visualElement);
    }
    public void Initialize(List<VisualElement> visualElements)
    {
        visualElements.ForEach(element =>
        {
            this.Add(element);
        });
    }
}
