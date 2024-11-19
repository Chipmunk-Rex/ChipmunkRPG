using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace Chipmunk.Library.EntityEditor
{
    public class EntityListView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EntityListView, VisualElement.UxmlTraits> { }
        EntityInspectorView inspectorView => GetRootElement(this).Q<EntityInspectorView>();
        public EntityListView()
        {
            DrawView();
        }
        private EntityListViewItem selectedItem = null;
        public void DrawView()
        {
            Clear();

            
            Button reloadBtn = new Button(() =>
            {
                DrawView();
            });
            reloadBtn.name = "reloadBtn";
            reloadBtn.text = "Reload";
            Add(reloadBtn);

            List<EntitySO> entitySOs = FindEntitySOs();
            foreach (EntitySO entitySO in entitySOs)
            {
                EntityListViewItem entityListItem = new EntityListViewItem();
                entityListItem.DrawView(entitySO, viewItem =>
                {
                    if (selectedItem != null)
                    {
                        selectedItem.SetSelected(false);
                    }
                    selectedItem = viewItem;
                    selectedItem.SetSelected(true);
                    Selection.activeObject = viewItem.entitySO;
                    inspectorView.UpdateInspactor(viewItem.entitySO);
                });
                Add(entityListItem);
            }
        }
        public List<EntitySO> FindEntitySOs()
        {
            List<EntitySO> entitySOList = new List<EntitySO>();
            AssetDatabase.FindAssets("", new[] { "Assets/EntityEditor/ScriptableObject" }).ToList().ForEach(guid =>
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                EntitySO scriptableObject = AssetDatabase.LoadAssetAtPath<EntitySO>(path);
                if (scriptableObject != null)
                {
                    entitySOList.Add(scriptableObject);
                }
            });
            return entitySOList;
        }
        public static VisualElement GetRootElement(VisualElement element)
        {
            VisualElement currentElement = element;
            while (currentElement.hierarchy.parent != null)
            {
                currentElement = currentElement.hierarchy.parent;
            }
            return currentElement;
        }
    }
}
