using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeDragAndDropManipulator : PointerManipulator
{
    private readonly float radius;
    private readonly VisualElement container;
    public float Angle { get; private set; }
    public Action<float> OnValueChanged;
    public RangeDragAndDropManipulator(VisualElement target, VisualElement container, float radius)
    {
        this.target = target;
        root = target.parent;

        this.radius = radius;
        this.container = container;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
        target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
        target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
        target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
        target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    private Vector2 targetStartPosition { get; set; }

    private Vector3 pointerStartPosition { get; set; }

    private bool enabled { get; set; }

    private VisualElement root { get; }

    private void PointerDownHandler(PointerDownEvent evt)
    {
        targetStartPosition = target.transform.position;
        pointerStartPosition = evt.position;
        target.CapturePointer(evt.pointerId);
        enabled = true;
    }

    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            Vector3 pointerDelta = evt.position - pointerStartPosition;

            target.transform.position = new Vector2(
                Mathf.Clamp(targetStartPosition.x + pointerDelta.x, 0, target.panel.visualTree.worldBound.width),
                Mathf.Clamp(targetStartPosition.y + pointerDelta.y, 0, target.panel.visualTree.worldBound.height));

            Vector2 targetSize = new Vector3(target.style.width.value.value, target.style.height.value.value);
            Vector2 containerSize = new Vector3(container.style.width.value.value, container.style.height.value.value);

            Vector2 centorPos = (Vector2)container.transform.position + containerSize / 2 - targetSize / 2;
            Vector2 direction = ((Vector2)target.transform.position - centorPos).normalized;
            target.transform.position = centorPos + direction * radius;

            float value = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            value += 90;
            if (value <= 0)
            {
                value += 360;
            }
            if (value > 360)
            {
                value -= 360;
            }

            if (value != this.Angle)
            {
                OnValueChanged?.Invoke(this.Angle);
            }
            this.Angle = value;
        }
    }

    private void PointerUpHandler(PointerUpEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            target.ReleasePointer(evt.pointerId);
        }
    }

    private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
    {
        if (enabled)
        {
            enabled = false;
        }
    }
}
