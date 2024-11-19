using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.EntityEditor
{
    public class EntityEditorSplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<EntityEditorSplitView, TwoPaneSplitView.UxmlTraits> { }
    }

}