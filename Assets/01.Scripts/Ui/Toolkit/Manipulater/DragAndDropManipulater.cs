using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragAndDropManipulator : PointerManipulator
{
    // Write a constructor to set target and store a reference to the
    // root of the visual tree.
    public DragAndDropManipulator(VisualElement target, VisualElement root = null)
    {
        this.target = target;

        this.root = root;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        // Register the four callbacks on target.
        target.RegisterCallback<MouseDownEvent>(MouseDownHandler);
        target.RegisterCallback<MouseMoveEvent>(MouseMoveHandler);
        target.RegisterCallback<MouseUpEvent>(MouseUpHandler);
    }


    protected override void UnregisterCallbacksFromTarget()
    {
        // Un-register the four callbacks from target.
        target.UnregisterCallback<MouseDownEvent>(MouseDownHandler);
        target.UnregisterCallback<MouseMoveEvent>(MouseMoveHandler);
        target.UnregisterCallback<MouseUpEvent>(MouseUpHandler);
    }


    private Vector2 dragStartPos { get; set; }
    private Vector2 targetStartWorldBound { get; set; }
    private Vector2 targetAfterWorldBound { get; set; }

    private Vector2 pointerStartPosition { get; set; }
    private Vector2 afterMovePosition;

    private bool enabled { get; set; }

    private VisualElement root { get; set; }
    VisualElement defaultParent;
    private void MouseDownHandler(MouseDownEvent evt)
    {
        Debug.Log("Down");
        if (root == null)
        {
            root = target.parent.parent;
        }

        dragStartPos = evt.localMousePosition;

        defaultParent = target.parent;
        root.Add(target);

        target.CaptureMouse();

        Vector2 offset = evt.mousePosition - root.worldBound.position - dragStartPos;

        target.style.position = Position.Absolute;
        target.style.left = offset.x;
        target.style.top = offset.y;

        enabled = true;
    }
    private void MouseMoveHandler(MouseMoveEvent evt)
    {
        Debug.Log("Move");
        if (enabled && target.HasMouseCapture())
        {
            Vector2 diff = evt.localMousePosition - dragStartPos;

            float x = target.layout.x;
            float y = target.layout.y;
            Debug.Log("move Suc" + diff);

            target.style.left = x + diff.x;
            target.style.top = y + diff.y;
        }
    }
    private void MouseUpHandler(MouseUpEvent evt)
    {
        if (enabled && target.HasMouseCapture())
        {
            defaultParent.Add(target);

            enabled = false;
            target.ReleaseMouse();
            target.style.position = Position.Relative;
            target.style.top = StyleKeyword.Null;
            target.style.left = StyleKeyword.Null;
        }
    }
    private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
    {
        if (enabled)
        {
            VisualElement slotsContainer = root.Q<VisualElement>("slots");
            UQueryBuilder<VisualElement> allSlots =
                slotsContainer.Query<VisualElement>(className: "slot");
            UQueryBuilder<VisualElement> overlappingSlots =
                allSlots.Where(OverlapsTarget);
            VisualElement closestOverlappingSlot =
                FindClosestSlot(overlappingSlots);
            Vector3 closestPos = Vector3.zero;
            if (closestOverlappingSlot != null)
            {
                closestPos = RootSpaceOfSlot(closestOverlappingSlot);
                closestPos = new Vector2(closestPos.x - 5, closestPos.y - 5);
            }
            target.transform.position =
                closestOverlappingSlot != null ?
                closestPos :
                dragStartPos;

            enabled = false;
        }
    }

    private bool OverlapsTarget(VisualElement slot)
    {
        return target.worldBound.Overlaps(slot.worldBound);
    }

    private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> slots)
    {
        List<VisualElement> slotsList = slots.ToList();
        float bestDistanceSq = float.MaxValue;
        VisualElement closest = null;
        foreach (VisualElement slot in slotsList)
        {
            Vector3 displacement =
                RootSpaceOfSlot(slot) - target.transform.position;
            float distanceSq = displacement.sqrMagnitude;
            if (distanceSq < bestDistanceSq)
            {
                bestDistanceSq = distanceSq;
                closest = slot;
            }
        }
        return closest;
    }

    private Vector3 RootSpaceOfSlot(VisualElement slot)
    {
        Vector2 slotWorldSpace = slot.parent.LocalToWorld(slot.layout.position);
        return root.WorldToLocal(slotWorldSpace);
    }
}