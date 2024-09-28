using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolResourceListView : ScrollView
{
    public new class UxmlFactory : UxmlFactory<PoolResourceListView, ScrollView.UxmlTraits> { }
    public Action<IPoolAble> onClickView;
    public void LoadView(List<IPoolAble> poolAbles)
    {
        this.Clear();
        poolAbles.ForEach(poolAble =>
        {
            PoolResourceView poolResourceView = new PoolResourceView();
            poolResourceView.LoadView(poolAble);
            poolResourceView.onClick += OnClickView;
            this.Add(poolResourceView);
        });
    }

    private void OnClickView(IPoolAble poolAble)
    {
        onClickView?.Invoke(poolAble);
    }
}
