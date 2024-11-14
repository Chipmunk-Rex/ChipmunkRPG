using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContextUI : BaseDocument
{
    ItemContainerView itemContainerView;
    HotbarView hotbarView;
    [SerializeField] EntityCompo playerCompo;
    private Player player => playerCompo.Entity as Player;
    public bool isShowingInventory { get; private set; }
    private void OnEnable()
    {

        itemContainerView = document.rootVisualElement.Q<ItemContainerView>();
        hotbarView = document.rootVisualElement.Q<HotbarView>();
        itemContainerView.parent.style.display = DisplayStyle.None;

        PlayerInputReader.Instance.onInventory += OpenInventory;
    }
    [ContextMenu("Open Inventory")]
    public void OpenInventory()
    {
        isShowingInventory = !isShowingInventory;
        itemContainerView.parent.style.display = isShowingInventory ? DisplayStyle.Flex : DisplayStyle.None;
        itemContainerView.DrawView(player.Inventory);

        hotbarView.InitializeView(new InventoryHotbar(player, player.Inventory, 5));

    }
}
