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
        public Action<PoolItemSO> onClickItem;
        public Action<PoolView> onClick;
        public Action<PoolView> onClickDelBtn;
        public Action<PoolItemSO, PoolSO> onClickItemDel;
        public void LoadView(PoolSO poolSO)
        {
            this.poolSO = poolSO;
            this.RegisterCallback<ClickEvent>(OnClick);

            VisualElement poolDataContainer = new VisualElement();
            poolDataContainer.name = "PoolDataContainer";
            this.Add(poolDataContainer);

            poolNameLbl = new Label();
            poolNameLbl.text = poolSO.name;
            poolDataContainer.Add(poolNameLbl);

            Button delBtn = new Button();
            delBtn.RegisterCallback<ClickEvent>(OnDelBtnClick);
            poolDataContainer.Add(delBtn);

            Foldout foldout = new Foldout();
            foldout.text = "PoolItems";
            this.Add(foldout);

            foreach (PoolItemSO poolItemSO in poolSO.itemList)
            {
                if (poolItemSO == null)
                    continue;
                    
                PoolItemView poolItemView = new PoolItemView();
                poolItemView.LoadView(poolItemSO);
                poolItemView.onClick += OnClickItem;
                poolItemView.onClickDel += So => OnClickDel(So, poolSO);
                foldout.Add(poolItemView);
            }
        }

        private void OnClickDel(PoolItemSO poolItemSO, PoolSO poolSO)
        {
            onClickItemDel?.Invoke(poolItemSO, poolSO);
        }

        private void OnClickItem(PoolItemSO poolItemSO)
        {
            onClickItem?.Invoke(poolItemSO);
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