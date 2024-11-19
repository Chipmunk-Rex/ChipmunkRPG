using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.ItemEditor
{
    public class ItemEditorView : VisualElement
    {
        public ItemEditorView()
        {
        }
        public new class UxmlFactory : UxmlFactory<ItemEditorView, VisualElement.UxmlTraits> { }
        // public Action i
        List<BaseItemSO> itemSOList = new();
        public ScrollView listView;
        public Action<BaseItemSO> onSelectItem;
        ItemView selectedItemView;
        public void Initialize()
        {
            listView = this.Q<ScrollView>();
            ReFreshView();
        }
        public void ReFreshView(BaseItemSO itemSO)
        {
            ReFreshData();

            listView.Clear();

            itemSOList.ForEach(item =>
            {
                ItemView itemView = new ItemView();
                itemView.Initialize(item);
                itemView.onClick += OnSelect;
                itemView.onDeleteButtonClick += OnDelete;
                listView.Add(itemView);

                if (itemSO != null && item == itemSO)
                {
                    OnSelect(itemView);
                }
            });
        }


        public void ReFreshView()
        {
            ReFreshView(null);
        }

        private void OnDelete(ItemView view)
        {
            Undo.DestroyObjectImmediate(view.itemSO);

            listView.Remove(view);
        }
        private void OnSelect(ItemView itemView)
        {
            if (selectedItemView != null)
                selectedItemView.RemoveClass("Selected");

            onSelectItem?.Invoke(itemView.itemSO);

            Selection.activeObject = itemView.itemSO;

            selectedItemView = itemView;
            itemView.AddClass("Selected");
        }

        public void ReFreshData()
        {
            itemSOList.Clear();
            AssetDatabase.FindAssets("", new[] { "Assets/ItemEditor/ScriptableObject" }).ToList().ForEach((Action<string>)(guid =>
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                BaseItemSO itemSo = AssetDatabase.LoadAssetAtPath<BaseItemSO>(path);
                if (itemSo != null)
                {
                    itemSOList.Add(itemSo);
                }
            }));
        }
    }
}