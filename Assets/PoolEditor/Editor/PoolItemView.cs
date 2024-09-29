using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolItemView : VisualElement
    {
        PoolItemSO poolItemSO;
        public Action<PoolItemSO> onClick;
        public Action<PoolItemSO> onClickDel;
        public PoolItemView()
        {
            this.RegisterCallback<ClickEvent>(OnClick);
        }

        private void OnClick(ClickEvent evt)
        {
            evt.StopPropagation();
            onClick?.Invoke(poolItemSO);
        }

        public void LoadView(PoolItemSO poolItemSO)
        {
            this.poolItemSO = poolItemSO;

            Label nameLbl = new Label();
            nameLbl.text = poolItemSO.poolName;
            this.Add(nameLbl);

            Button delBtn = new Button();
            delBtn.RegisterCallback<ClickEvent>(OnClickDelBtn);
            delBtn.text = "Delete";
            this.Add(delBtn);

        }

        private void OnClickDelBtn(ClickEvent evt)
        {
            onClickDel?.Invoke(poolItemSO);
        }
    }
}