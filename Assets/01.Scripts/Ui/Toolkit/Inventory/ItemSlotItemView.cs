using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlotItemView : VisualElement
{
    VisualElement visual;
    public ItemSlotItemView()
    {
        visual = new VisualElement();
        this.Add(visual);
    }
    public void SetSprite(Sprite sprite)
    {
        StyleBackground backgroundStyle = visual.style.backgroundImage;
        Background background = backgroundStyle.value;
        background.sprite = sprite;

        visual.style.backgroundImage = background;
    }
}
