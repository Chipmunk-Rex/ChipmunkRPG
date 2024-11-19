using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Chipmunk.Library.EntityEditor
{
    public class EntityInspectorView : VisualElement
    {
        public static EntityInspectorView Instace;
        public EntityInspectorView()
        {
            Instace = this;
        }
        public new class UxmlFactory : UxmlFactory<EntityInspectorView, VisualElement.UxmlTraits> { }
        Editor editor;
        public Action onDataChange;
        public void UpdateInspactor(EntitySO entitySO)
        {
            this.Clear();
            UnityEngine.Object.DestroyImmediate(editor);

            if (entitySO == null) return;

            editor = Editor.CreateEditor(entitySO);
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