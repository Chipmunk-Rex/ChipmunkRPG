using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : BaseDocument
{
    HotbarView hotbarView;

    [SerializeField] EntityCompo playerCompo;
    private Player player => playerCompo.Entity as Player;
    void OnEnable()
    {
        hotbarView = document.rootVisualElement.Q<HotbarView>();

        Debug.Log("startUI");
        if (player != null)
        {
            Debug.Log("startUIm");
            InitializeHotbar();
        }
        else
        {
            playerCompo.OnSpawnEvent.AddListener(() => InitializeHotbar());
        }
    }

    private void InitializeHotbar()
    {
        hotbarView.InitializeView(player.InventoryHotbar);
        Debug.Log("InitializedHotbar");
    }
}
