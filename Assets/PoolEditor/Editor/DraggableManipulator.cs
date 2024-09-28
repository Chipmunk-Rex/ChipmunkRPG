using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class DraggableManipulator : Manipulator
{
    private Vector2 m_Start;

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    private void OnMouseDown(MouseDownEvent evt)
    {
        if (evt.button == 0) // 왼쪽 마우스 버튼
        {
            m_Start = evt.localMousePosition;
            target.CaptureMouse();
            evt.StopImmediatePropagation();
        }
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (target.HasMouseCapture())
        {
            Vector2 delta = evt.localMousePosition - m_Start;
            target.style.left = target.style.left.value.value + delta.x;
            target.style.top = target.style.top.value.value + delta.y;
            m_Start = evt.localMousePosition;
            evt.StopPropagation();
        }
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        if (evt.button == 0 && target.HasMouseCapture())
        {
            target.ReleaseMouse();
            evt.StopPropagation();
        }
    }
}