using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlotItemView : VisualElement
{
    VisualElement visual;
    public Label countLbl;
    public readonly Item item;
    public ItemSlotItemView(Item item)
    {
        this.item = item;

        this.AddManipulator(new DragAndDropManipulator(this));

        visual = new VisualElement();
        visual.name = "ItemVisual";
        this.Add(visual);

        countLbl = new Label("0");
        countLbl.visible = false;
        countLbl.name = "ItemCount";
        this.Add(countLbl);
    }
    public void SetSprite(Sprite sprite)
    {
        StyleBackground backgroundStyle = visual.style.backgroundImage;
        Background background = backgroundStyle.value;
        background.sprite = sprite;

        visual.style.backgroundImage = background;
    }
    public void SetItemCount(int count)
    {

        if (count == 0)
        {
            countLbl.visible = false;
        }
        countLbl.text = count.ToString();
    }
}
