using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.BehaviourTreeEditor
{
    public class BT_SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<BT_SplitView, TwoPaneSplitView.UxmlTraits> { }
        public BT_SplitView() : base()
        {
            this.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            if (fixedPane != null)
                fixedPaneInitialDimension = fixedPane.style.minWidth.value.value;
        }

        public BT_InspectorView inspectorView;
    }
}