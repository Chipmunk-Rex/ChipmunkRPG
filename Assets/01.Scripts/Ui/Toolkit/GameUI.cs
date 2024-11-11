using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : BaseDocument
{
    [SerializeField] Player player;
    HotbarView hotbarView;
    void OnEnable()
    {
        hotbarView = document.rootVisualElement.Q<HotbarView>();
        // hotbarView.InitializeView(player.InventoryCompo.Hotbar);
    }
}
