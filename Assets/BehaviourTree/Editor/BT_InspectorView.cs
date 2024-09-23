using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class BT_InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BT_InspectorView, VisualElement.UxmlTraits> { }
    Editor editor;
    public BT_InspectorView()
    {

    }

    internal void UpdateSelection(BT_NodeView nodeView)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(nodeView.node);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            if (editor.target != null)
                editor.OnInspectorGUI();
        }
        );
        Add(container);
    }
}
