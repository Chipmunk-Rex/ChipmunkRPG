using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeDragAndDropManipulator : PointerManipulator
{
    private readonly float radius;
    private readonly VisualElement container;
    public float _angle;
    public float Angle
    {
        get { return _angle; }
        set
        {
            float angle = value;

            if (angle <= 0)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }
            angle = ClampAngle(angle, minAngle, maxAngle);

            SetPos(angle);
            _angle = angle;
        }
    }
    public Action<float> onAngleChanged;
    public float minAngle = 0f;
    public float maxAngle = 360f;
    public RangeDragAndDropManipulator(VisualElement target, VisualElement container, float radius)
    {
        this.target = target;
        root = target.parent;

        this.radius = radius;
        this.container = container;

        GetDir();
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
        GetDir();
    }

    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            Vector3 pointerDelta = evt.position - pointerStartPosition;

            target.transform.position = new Vector2(
                Mathf.Clamp(targetStartPosition.x + pointerDelta.x, 0, target.panel.visualTree.worldBound.width),
                Mathf.Clamp(targetStartPosition.y + pointerDelta.y, 0, target.panel.visualTree.worldBound.height));

            Vector2 direction = GetDir();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // value += 90;
            if (angle <= 0)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }
            angle = ClampAngle(angle, minAngle, maxAngle);

            if (angle != this.Angle)
            {
                onAngleChanged?.Invoke(this.Angle);
            }
            this.Angle = angle;
        }
    }
    public float ClampAngle(float angle, float minAngle, float maxAngle)
    {
        float calculatedAngle = angle;
        if (angle != Mathf.Clamp(angle, minAngle, maxAngle))
        {
            float minAbs = Mathf.Abs(angle - minAngle);
            float maxAbs = Mathf.Abs(angle - maxAngle);
            if (minAngle == 0)
            {
                minAbs = Mathf.Abs(angle - (minAngle + 360));
                Debug.Log(minAbs + " " + maxAbs);
                // minAngle = 360;
            }else
            if (maxAngle == 360)
            {
                maxAbs = Mathf.Abs(angle - (maxAngle - 360));
                // maxAngle = 0;
            }
            calculatedAngle = minAbs < maxAbs ? minAngle : maxAngle;
            Debug.Log("calculatedAngle" + calculatedAngle);
        }

        return calculatedAngle;
    }
    public void SetPos(float angle)
    {
        Vector2 targetSize = new Vector3(target.style.width.value.value, target.style.height.value.value);
        Vector2 containerSize = new Vector3(container.style.width.value.value, container.style.height.value.value);

        Vector2 centorPos = (Vector2)container.transform.position + containerSize / 2 - targetSize / 2;

        float angleRad = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRad);
        float y = Mathf.Sin(angleRad);
        Vector2 dir = new Vector2(x, y);

        target.transform.position = centorPos + dir * radius;
    }
    private Vector2 GetDir()
    {
        Vector2 targetSize = new Vector3(target.style.width.value.value, target.style.height.value.value);
        Vector2 containerSize = new Vector3(container.style.width.value.value, container.style.height.value.value);

        Vector2 centorPos = (Vector2)container.transform.position + containerSize / 2 - targetSize / 2;
        Vector2 direction = ((Vector2)target.transform.position - centorPos).normalized;
        return direction;
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
