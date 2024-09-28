using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.ItemEditor
{
    public class ItemSplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<ItemSplitView, TwoPaneSplitView.UxmlTraits> { };
    }
}