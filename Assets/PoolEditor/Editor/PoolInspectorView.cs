using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolInspectorView : IMGUIContainer
    {
        public new class UxmlFactory : UxmlFactory<PoolInspectorView, IMGUIContainer.UxmlTraits> { }
        Editor editor;
        public Action onDataChange;
        public void Draw<T>(T @object) where T : UnityEngine.Object
        {
            this.Clear();
            UnityEngine.Object.DestroyImmediate(editor);

            if (@object == null) return;

            editor = Editor.CreateEditor(@object);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (editor.target != null)
                    editor.OnInspectorGUI();
            });
            BindingExtensions.TrackSerializedObjectValue(container, editor.serializedObject, TrackValue);
            Add(container);
        }

        private void TrackValue(SerializedObject @object)
        {
            EditorUtility.SetDirty(@object.targetObject);
            onDataChange?.Invoke();
        }
    }
}
