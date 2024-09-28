using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<PoolView, VisualElement.UxmlTraits> { }
        public PoolSO poolSO { get; private set; }
        Label poolNameLbl;
        public Action<PoolView> onClick;
        public Action<PoolView> onClickDelBtn;
        public void LoadView(PoolSO poolSO)
        {
            this.poolSO = poolSO;
            this.RegisterCallback<ClickEvent>(OnClick);

            poolNameLbl = new Label();
            poolNameLbl.text = poolSO.name;
            this.Add(poolNameLbl);

            Button delBtn = new Button();
            delBtn.RegisterCallback<ClickEvent>(OnDelBtnClick);
            this.Add(delBtn);
        }

        private void OnClick(ClickEvent evt)
        {
            onClick?.Invoke(this);
        }

        private void OnDelBtnClick(ClickEvent evt)
        {
            onClickDelBtn?.Invoke(this);
        }
    }
}