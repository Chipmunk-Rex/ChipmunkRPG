using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolCreateView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<PoolCreateView, VisualElement.UxmlTraits> { }
        private TextField textField;
        public Action<string> onCreateBtnClick;
        public PoolCreateView()
        {
            this.style.flexDirection = FlexDirection.Row;

            Label label = new Label();
            label.text = "Create Pool";
            label.AddToClassList("PoolCreateTitle");
            this.Add(label);

            textField = new TextField();
            textField.AddToClassList("PoolCreateField");
            textField.value = "PoolName";
            this.Add(textField);

            Button createBtn = new Button();
            createBtn.RegisterCallback<ClickEvent>(OnButtonClick);
            createBtn.text = "Create";
            createBtn.AddToClassList("PoolCreateBtn");
            this.Add(createBtn);
        }

        private void OnButtonClick(ClickEvent evt)
        {
            onCreateBtnClick?.Invoke(textField.value);
            textField.value = "";
        }
    }
}