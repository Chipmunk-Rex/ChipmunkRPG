using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldTimeView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<WorldTimeView, VisualElement.UxmlTraits> { }
    public WorldTimeView()
    {

    }
    public void DrawView(World world)
    {
        Clear();
        world.Time.OnvalueChanged.AddListener(OnWorldTimeChanged);
        // world.Time = 0;
        // var timeLabel = new Label($"Time: {time}");
        // Add(timeLabel);

    }

    private void OnWorldTimeChanged(int prev, int next)
    {
        
    }
}
