using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.BehaviourTreeEditor
{
    public class BT_NodeView : Node
    {
        public BT_NodeView(BT_Node node) : base("Assets/BehaviourTree/Editor/NodeViewEditor.uxml")
        {
            this.node = node;
            this.title = NodeNameCreator(node.name);
            this.viewDataKey = node.guid;
            // this.capabilities = Capabilities.Deletable & Capabilities.Selectable;

            style.left = node.position.x;
            style.top = node.position.y;

            CreateNodeInputPorts();
            CreateNodeOutputPorts();
            AddToClassList("BT_NodeView");
            
            SetDesc();
        }

        public BT_Node node;
        public Port input;
        public Port output;
        public Action<BT_NodeView> onNodeSeleted;
        public Orientation orientation = Orientation.Vertical;
        public void SetDesc()
        {
            this.Q<Label>("Description").text = node.nodeDescription;
        }
        public static string NodeNameCreator(string name)
        {
            string[] splitName = name.Split("_");
            if (splitName.Length == 1)
                return name;
            return splitName[1];
        }
        private void CreateNodeInputPorts()
        {
            if (!(node is BT_RootNode))
                input = InstantiatePort(orientation, Direction.Input, Port.Capacity.Single, typeof(bool));
            // if (node is BT_ActionNode)
            // {
            // input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            // }

            if (input != null)
            {
                input.portName = "";

                Label portLbl = input.ElementAt(1) as Label;
                portLbl.style.marginLeft = 0;
                portLbl.style.marginRight = 0;

                inputContainer.Add(input);
            }
        }
        private void CreateNodeOutputPorts()
        {
            if (node is BT_CompositeNode)
            {
                output = InstantiatePort(orientation, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if (node is BT_DecoratorNode)
            {
                output = InstantiatePort(orientation, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (node is BT_RootNode)
            {
                output = InstantiatePort(orientation, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if (output != null)
            {
                output.portName = "";

                Label portLbl = output.ElementAt(1) as Label;
                portLbl.style.marginLeft = 0;
                portLbl.style.marginRight = 0;

                outputContainer.Add(output);
            }
        }

        public void UpdateNodeView()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("success");
            RemoveFromClassList("failure");

            if (Application.isPlaying)
                switch (node.state)
                {
                    case BT_EnumNodeState.Running:
                        if (node.isStarted)
                            AddToClassList("running");
                        break;
                    case BT_EnumNodeState.Success:
                        AddToClassList("success");
                        break;
                    case BT_EnumNodeState.Failure:
                        AddToClassList("failure");
                        break;
                }
        }

        public override void OnSelected()
        {
            base.OnSelected();
            onNodeSeleted?.Invoke(this);
        }
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(node, "BT_NodeView SetPosition");
            node.position = newPos.min;
            EditorUtility.SetDirty(node);
        }
        public void SortChildren()
        {
            BT_CompositeNode compositeNode = node as BT_CompositeNode;
            if (compositeNode != null)
            {
                compositeNode.GetChild().Sort(SortByHorizontalPos);
            }
        }

        private int SortByHorizontalPos(BT_Node x, BT_Node y)
        {
            return x.position.x < y.position.x ? -1 : 1;
        }
    }
}