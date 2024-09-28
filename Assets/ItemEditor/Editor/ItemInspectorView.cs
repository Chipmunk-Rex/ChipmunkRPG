using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.ItemEditor
{
    public class ItemInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ItemInspectorView, VisualElement.UxmlTraits> { }
        Editor editor;
        public Action onDataChange;
        public void UpdateInspactor(BaseItemSO baseItemSO)
        {
            Debug.Log("Update");
            this.Clear();
            UnityEngine.Object.DestroyImmediate(editor);

            if (baseItemSO == null) return;

            editor = Editor.CreateEditor(baseItemSO);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (editor.target != null)
                    editor.OnInspectorGUI();
            });
            BindingExtensions.TrackSerializedObjectValue(container, editor.serializedObject, serialzedObject => onDataChange?.Invoke());
            Add(container);
        }
    }
}