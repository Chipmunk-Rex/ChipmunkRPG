using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : BaseDocument
{
    ItemContainerView itemContainerView;
    Player player;
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
        itemContainerView.DrawView(player.inventory);
    }
}
