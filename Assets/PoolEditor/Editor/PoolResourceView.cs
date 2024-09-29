using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolResourceView : VisualElement
    {
        IPoolAble poolAble;
        Label poolAbleNameLbl;
        public Action<IPoolAble> onClickBtn;
        public Action<IPoolAble> onClick;
        public PoolResourceView()
        {
            poolAbleNameLbl = new Label();
            this.Add(poolAbleNameLbl);
            this.RegisterCallback<ClickEvent>(OnClick);

            Button addResourceBtn = new Button();
            addResourceBtn.RegisterCallback<ClickEvent>(OnClickBtn);
            addResourceBtn.text = "Add";
            this.Add(addResourceBtn);
        }

        private void OnClick(ClickEvent evt)
        {
            onClick?.Invoke(poolAble);
        }

        private void OnClickBtn(ClickEvent evt)
        {
            onClickBtn?.Invoke(poolAble);
            evt.StopPropagation();
        }

        public void LoadView(IPoolAble poolAble)
        {
            this.poolAble = poolAble;
            poolAbleNameLbl.text = poolAble.PoolName;
        }
    }
}