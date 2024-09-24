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
    BT_InspectorViewHeader inspectorViewHeader;
    public BT_InspectorView()
    {
        inspectorViewHeader = this.Q<BT_InspectorViewHeader>();
    }

    public void UpdateSelection(BT_NodeView nodeView)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);

        if (nodeView == null || nodeView.node == null)
        {
            return;
        }
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
