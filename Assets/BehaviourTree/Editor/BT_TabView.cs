using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace Chipmunk.Library.BehaviourTreeEditor
{
    public class BT_TabedView : TabbedView
    {
        public new class UxmlFactory : UxmlFactory<BT_TabedView> { }
        public BT_TabedView()
        {

        }
    }
}