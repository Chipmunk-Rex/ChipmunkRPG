using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingUI : VisualElement
{
    public new class UXMLFactory : UxmlFactory<SettingUI, VisualElement.UxmlTraits> { }
    public SettingUI()
    {
        VisualElement buttonContainer = new VisualElement();
        buttonContainer.name = "buttonContainer";
        {
            VisualElement saveButton = new VisualElement();
            saveButton.name = "saveButton";

            saveButton.RegisterCallback<ClickEvent>(OnSaveButtonClick);

            buttonContainer.Add(saveButton);

            Label label = new Label("Save");
            label.AddToClassList("buttonLabel");
            label.style.alignSelf = Align.Center;
            label.style.fontSize = 40;
            saveButton.Add(label);
        }

        {
            VisualElement exitButton = new VisualElement();
            exitButton.name = "exitButton";

            exitButton.RegisterCallback<ClickEvent>(OnExitButtonClick);

            buttonContainer.Add(exitButton);
            Label label = new Label("Exit");
            label.style.alignSelf = Align.Center;
            label.style.fontSize = 40;
            label.AddToClassList("buttonLabel");
            exitButton.Add(label);
        }
        this.Add(buttonContainer);
    }

    private void OnExitButtonClick(ClickEvent evt)
    {
        World.Instance.Save();
        Application.Quit();
    }

    private void OnSaveButtonClick(ClickEvent evt)
    {
        World.Instance.Save();
    }
}
