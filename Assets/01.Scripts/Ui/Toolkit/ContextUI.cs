using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContextUI : BaseDocument
{
    ItemContainerView itemContainerView;
    HotbarView hotbarView;
    Player player;
    public bool isShowingInventory { get; private set; }
    private void OnEnable()
    {
        itemContainerView = document.rootVisualElement.Q<ItemContainerView>();
        hotbarView = document.rootVisualElement.Q<HotbarView>();

        if (player == null)
        {
            player = GameObject.FindAnyObjectByType<Player>();
        }
        itemContainerView.parent.style.display = DisplayStyle.None;
    }
    public void OpenInventory()
    {
        isShowingInventory = !isShowingInventory;
        itemContainerView.parent.style.display = isShowingInventory ? DisplayStyle.Flex : DisplayStyle.None;
        itemContainerView.DrawView(player.inventory);

        hotbarView.InitializeView(player.inventory.Hotbar);
    }
}
