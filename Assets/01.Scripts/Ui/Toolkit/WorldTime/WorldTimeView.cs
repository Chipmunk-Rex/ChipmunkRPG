using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldTimeView : VisualElement
{
    World world;
    VisualElement timeArrow;
    VisualElement cycleArrow;
    Label timeLabel;
    public new class UxmlFactory : UxmlFactory<WorldTimeView, VisualElement.UxmlTraits> { }
    public WorldTimeView()
    {
        CreateElement();

    }
    public void DrawView(World world)
    {
        this.world = world;
        world.Time.OnvalueChanged.AddListener(OnWorldTimeChanged);
        // world.Time = 0;
        // var timeLabel = new Label($"Time: {time}");
        // Add(timeLabel);


    }

    private void CreateElement()
    {
        timeLabel = new Label("000");
        timeLabel.name = "worldTimeLabel";

        timeArrow = new VisualElement();
        timeArrow.name = "timeArrow";

        cycleArrow = new VisualElement();
        cycleArrow.name = "cycleArrow";

        this.Add(timeLabel);
        this.Add(cycleArrow);
        this.Add(timeArrow);
    }

    private void OnWorldTimeChanged(int prev, int next)
    {

        float timeRotate = ((next + (world.Day * world.worldSO.dayDuration)) / (float)world.worldSO.dayDuration) * 360;
        StyleRotate timeRotateStyle = new StyleRotate(new Rotate(timeRotate));
        timeArrow.style.rotate = timeRotateStyle;

        float cycleRotate = world.Day / (float)world.worldSO.cycleDuration * 360;
        StyleRotate cycleRotateStyle = new StyleRotate(new Rotate(cycleRotate));
        cycleArrow.style.rotate = cycleRotateStyle;

        timeLabel.text = $"{Mathf.Clamp(world.Day,0, 999).ToString("000")}";
    }
}
