using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContextUI : BaseDocument
{
    ItemContainerView itemContainerView;
    HotbarView hotbarView;
    SettingUI settingUI;
    CraftView craftView;
    [SerializeField] EntityCompo playerCompo;
    private Player player => playerCompo.Entity as Player;
    public bool isShowingInventory { get; private set; }
    public bool isShowingCraft { get; private set; }
    public bool isShowingSetting { get; private set; }
    private void OnEnable()
    {

        itemContainerView = document.rootVisualElement.Q<ItemContainerView>();
        itemContainerView.parent.style.display = DisplayStyle.None;

        hotbarView = document.rootVisualElement.Q<HotbarView>();

        craftView = document.rootVisualElement.Q<CraftView>();

        settingUI = document.rootVisualElement.Q<SettingUI>();

        PlayerInputReader.Instance.onInventory += OpenInventory;
        UIInputReader.Instance.onCraft += OpenCraft;

        UIInputReader.Instance.onSetting += OpenSetting;
    }

    private void OpenSetting()
    {
        settingUI.style.display = settingUI.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;
        
        CloseOtherUI(2);
    }
    public void CloseOtherUI(int index)
    {
        if (index != 0)
        {
            isShowingCraft = false;
            craftView.CloseView();
        }
        if (index != 1)
        {
            isShowingInventory = false;
            itemContainerView.parent.style.display = DisplayStyle.None;
        }
        if (index != 2)
        {
            isShowingSetting = false;
            settingUI.style.display = DisplayStyle.None;
        }
    }

    public void OpenCraft(ItemCrafter itemCrafter)
    {
        isShowingCraft = !isShowingCraft;
        if (isShowingCraft == false)
            craftView.CloseView();
        else
            craftView.OpenView(itemCrafter, player.Inventory);

        CloseOtherUI(0);
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

        CloseOtherUI(1);
    }
}
