using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContextUI : BaseDocument
{
    ItemContainerView itemContainerView;
    HotbarView hotbarView;

    CraftView craftView;
    [SerializeField] EntityCompo playerCompo;
    private Player player => playerCompo.Entity as Player;
    public bool isShowingInventory { get; private set; }
    public bool isShowingCraft { get; private set; }
    private void OnEnable()
    {

        itemContainerView = document.rootVisualElement.Q<ItemContainerView>();
        itemContainerView.parent.style.display = DisplayStyle.None;

        hotbarView = document.rootVisualElement.Q<HotbarView>();

        craftView = document.rootVisualElement.Q<CraftView>();

        PlayerInputReader.Instance.onInventory += OpenInventory;
        UIInputReader.Instance.onCraft += OpenCraft;
    }

    public void OpenCraft(ItemCrafter itemCrafter)
    {
        isShowingCraft = !isShowingCraft;
        if (isShowingCraft == false)
            craftView.CloseView();
        else
            craftView.OpenView(itemCrafter, player.Inventory);
    }
    private void OpenCraft()
    {
        OpenCraft(player.ItemCrafter);
    }
    public void OpenInventory()
    {
        isShowingInventory = !isShowingInventory;
        itemContainerView.parent.style.display = isShowingInventory ? DisplayStyle.Flex : DisplayStyle.None;
        itemContainerView.DrawView(player.Inventory);

        hotbarView.InitializeView(new InventoryHotbar(player, player.Inventory, 5));

    }
}
