using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;
using UnityEditor;
public class BT_TabView : TabbedView
{

    public new class UxmlFactory : UxmlFactory<BT_InspectorView, VisualElement.UxmlTraits> { }
    public BT_TabView()
    {

    }
}
