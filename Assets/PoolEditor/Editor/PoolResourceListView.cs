using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolResourceListView : ScrollView
    {
        public new class UxmlFactory : UxmlFactory<PoolResourceListView, ScrollView.UxmlTraits> { }
        public Action<IPoolAble> onClickViewBtn;
        public Action<IPoolAble> onClickView;
        public void LoadView(List<IPoolAble> poolAbles)
        {
            this.Clear();
            poolAbles.ForEach(poolAble =>
            {
                PoolResourceView poolResourceView = new PoolResourceView();
                poolResourceView.LoadView(poolAble);
                poolResourceView.onClickBtn += OnClickViewBtn;
                poolResourceView.onClick += OnClickView;
                this.Add(poolResourceView);
            });
        }

        private void OnClickView(IPoolAble poolAble)
        {
            onClickView?.Invoke(poolAble);
        }

        private void OnClickViewBtn(IPoolAble poolAble)
        {
            onClickViewBtn?.Invoke(poolAble);
        }
    }
}