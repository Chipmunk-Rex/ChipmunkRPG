using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemInspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ItemInspectorView, VisualElement.UxmlTraits> { }
    Editor editor;
    public void UpdateInspactor(BaseItemSO baseItemSO)
    {
        Debug.Log("Update");
        this.Clear();
        Object.DestroyImmediate(editor);

        if (baseItemSO == null) return;

        editor = Editor.CreateEditor(baseItemSO);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            if (editor.target != null)
                editor.OnInspectorGUI();
        });

        
        container.TrackSerializedObjectValue()
        Add(container);
    }
}
