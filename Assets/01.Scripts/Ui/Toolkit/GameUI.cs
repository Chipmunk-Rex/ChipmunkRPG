using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : BaseDocument
{
    HotbarView hotbarView;
    MeterView meterView;
    [SerializeField] EntityCompo playerCompo;
    private Player player => playerCompo.Entity as Player;
    void OnEnable()
    {
        hotbarView = document.rootVisualElement.Q<HotbarView>();
        meterView = document.rootVisualElement.Q<MeterView>();

        Debug.Log("startUI");
        TryInitialize();
    }

    private void TryInitialize()
    {
        if (player != null)
        {
            Debug.Log("startUIm");
            Initialize();
        }
        else
        {
            playerCompo.OnSpawnEvent.AddListener(Initialize);
        }
    }

    private void Initialize()
    {
        InitializeHotbar();
        meterView.Initailize(player.meters);
    }

    private void InitializeHotbar()
    {
        hotbarView.InitializeView(player.InventoryHotbar);
    }
}
