using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.BuildingEditor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }
        public InspectorView()
        {

        }
        Editor editor;
        public Action<SerializedObject> onTrackValue;
        public void DrawInspector<T>(T tObject) where T : UnityEngine.Object
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);

            if (tObject == null)
                return;
            editor = Editor.CreateEditor(tObject);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (editor.target != null)
                    editor.OnInspectorGUI();
            }
            );
            Add(container);
            BindingExtensions.TrackSerializedObjectValue(container, editor.serializedObject, OnTrackValue);
            Add(container);
        }
        public void HideInspector()
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(editor);
        }

        private void OnTrackValue(SerializedObject @object)
        {
            onTrackValue?.Invoke(@object);
        }
    }
}