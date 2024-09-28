using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.BehaviourTreeEditor
{
    public class BT_TabButton : TabButton
    {
        public new class UxmlFactory : UxmlFactory<BT_TabButton, TabButton.UxmlTraits> { }
        public BT_TabButton()
        {

        }
    }
}