using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContextUI : BaseDocument
{
    ItemContainerView itemContainerView;
    Player player;
    public bool isShowingInventory { get; private set; }
    private void OnEnable()
    {
        itemContainerView = document.rootVisualElement.Q<ItemContainerView>();
        if (player == null)
        {
            player = GameObject.FindAnyObjectByType<Player>();
        }
    }
    public void OpenInventory()
    {
        isShowingInventory = !isShowingInventory;
        itemContainerView.style.display = isShowingInventory ? DisplayStyle.Flex : DisplayStyle.None;
        itemContainerView.DrawView(player.inventory);
    }
}
