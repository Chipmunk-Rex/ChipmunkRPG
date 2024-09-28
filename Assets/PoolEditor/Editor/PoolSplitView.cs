using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolSplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<PoolSplitView, TwoPaneSplitView.UxmlTraits> { }
    }
}