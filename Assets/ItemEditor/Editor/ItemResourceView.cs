using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemResourceView : VisualElement
{
    // private List<>
    public new class UxmlFactory : UxmlFactory<ItemResourceView, VisualElement.UxmlTraits> { }
    public ItemResourceView()
    {
        ItemResourceFold itemResourceFold = new ItemResourceFold();
        this.Add(itemResourceFold);
    }
}
