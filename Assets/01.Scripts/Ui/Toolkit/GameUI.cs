using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : BaseDocument
{
    HotbarView hotbarView;
    
    [SerializeField] EntityCompo playerCompo;
    private Player player;
    void OnEnable()
    {
        player = playerCompo.Entity as Player;
        hotbarView = document.rootVisualElement.Q<HotbarView>();

        hotbarView.InitializeView(new PlayerInventoryHotbar(player, player.Inventory, 5));
    }
}
