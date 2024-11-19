using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.EntityEditor
{
    public class EntitySOView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EntitySOView, VisualElement.UxmlTraits> { }
        public Dictionary<Type, EntitySOFold> entitySOFolds = new();
        public EntitySOView()
        {
            DrawView();
        }

        private void DrawView()
        {
            Clear();
            CreateElement();
        }

        private void CreateElement()
        {
            Type baseType = typeof(EntitySO);
            entitySOFolds.Clear();

            TypeCache.TypeCollection typeCollect = TypeCache.GetTypesDerivedFrom(baseType);
            foreach (Type cachedType in typeCollect)
            {
                EntitySOFold itemResourceFold = new EntitySOFold();
                itemResourceFold.DrawView(cachedType);
                entitySOFolds.Add(cachedType, itemResourceFold);
            }

            foreach (Type cachedType in typeCollect)
            {
                if (cachedType == baseType)
                {
                    continue;
                }
                if (entitySOFolds.ContainsKey(cachedType.BaseType))
                {
                    entitySOFolds[cachedType.BaseType].element.Add(entitySOFolds[cachedType]);
                }
                else
                {
                    this.Add(entitySOFolds[cachedType]);
                }
            }

        }
    }
}
