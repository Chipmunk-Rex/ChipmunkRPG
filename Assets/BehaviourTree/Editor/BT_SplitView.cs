using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BT_SplitView : TwoPaneSplitView 
{
    public new class UxmlFactory : UxmlFactory<BT_SplitView, TwoPaneSplitView.UxmlTraits> { }
    public BT_InspectorView inspectorView;
}