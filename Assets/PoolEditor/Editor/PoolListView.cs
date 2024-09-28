using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolListView : ScrollView
    {
        public new class UxmlFactory : UxmlFactory<PoolListView, ScrollView.UxmlTraits> { }
        public PoolView selectedView;
        public event Action<PoolView> onSelect;
        public event Action<PoolSO> onDelete;
        public void LoadView(List<PoolSO> poolSOs)
        {
            this.Clear();
            foreach (PoolSO poolSO in poolSOs)
            {
                PoolView poolView = CreatePoolView(poolSO);
                this.Add(poolView);
            }
        }
        private PoolView CreatePoolView(PoolSO poolSO)
        {
            PoolView poolView = new PoolView();
            poolView.LoadView(poolSO);
            poolView.onClick += OnClickView;
            poolView.onClickDelBtn += OnClickDel;
            return poolView;
        }

        private void OnClickDel(PoolView view)
        {
            onDelete?.Invoke(view.poolSO);
            this.Remove(view);
        }

        private void OnClickView(PoolView view)
        {
            view.AddToClassList("OnSelected");
            onSelect?.Invoke(view);
        }
    }

}