using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolResourceView : VisualElement
{
    IPoolAble poolAble;
    Label poolAbleNameLbl;
    public Action <IPoolAble> onClick;
    public PoolResourceView()
    {
        poolAbleNameLbl = new Label();
        this.Add(poolAbleNameLbl);

        Button addResourceBtn = new Button();
        addResourceBtn.RegisterCallback<ClickEvent>(OnClick);
        addResourceBtn.text = "Add";
        this.Add(addResourceBtn);
    }

    private void OnClick(ClickEvent evt)
    {
        onClick?.Invoke(poolAble);
    }

    public void LoadView(IPoolAble poolAble)
    {
        this.poolAble = poolAble;
        poolAbleNameLbl.text = poolAble.PoolName;
    }
}
