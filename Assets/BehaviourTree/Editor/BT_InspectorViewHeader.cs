using System.Collections;
using System.Collections.Generic;
using log4net.Util;
using UnityEngine;
using UnityEngine.UIElements;

public class BT_InspectorViewHeader : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BT_InspectorViewHeader, VisualElement.UxmlTraits> { }
    BT_NodeView selectedNodeView;
    public BT_InspectorViewHeader()
    {
        this.style.display = DisplayStyle.None;
    }
    public Label titleLbl = null;
    public void Initialize()
    {
        if (titleLbl == null)
            titleLbl = this.Q<Label>();
        if (selectedNodeView == null)
            this.style.display = DisplayStyle.None;
    }

    public void UpdateSelection(BT_NodeView nodeView)
    {
        selectedNodeView = nodeView;
        if (nodeView == null)
        {
            this.style.display = DisplayStyle.None;
            return;
        }
        this.style.display = DisplayStyle.Flex;
        if (titleLbl == null)
            titleLbl = this.Q<Label>();
        titleLbl.text = BT_NodeView.NodeNameCreator(nodeView.node.GetType().ToString());
    }
}
