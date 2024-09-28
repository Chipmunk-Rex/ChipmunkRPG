using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System;
using UnityEditor;

namespace Chipmunk.Library.ItemEditor
{
    public class ItemResourceFold : VisualElement
    {
        Type type;
        public VisualElement element { get; private set; }
        public Action<Type> onClick;
        public ItemResourceFold()
        {
            this.style.flexDirection = FlexDirection.Row;
            this.AddToClassList("ResourceViewParent");
        }
        public void Initialize(Type type)
        {
            this.type = type;




            CreateElement();
            CreateButton();
        }

        private void CreateElement()
        {
            bool isRoot = TypeCache.GetTypesDerivedFrom(type).Count == 0;

            if (isRoot)
            {
                Label label = new Label();
                label.text = $"     {type.ToString()}";
                // label.style.paddingLeft = 18;
                element = label;
            }
            else
            {
                Foldout foldout = new Foldout();
                foldout.text = $"{type.ToString()}";
                element = foldout;
            }
            element.AddToClassList("ResourceView");
            this.Add(element);
        }

        public void CreateButton()
        {
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
}