using System;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library.PoolEditor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolEditorWindow : EditorWindow
    {
        PoolInspectorView inspectorView;
        PoolListView poolListView;
        PoolCreateView poolCreateView;
        ObjectField objectField;
        Button reloadResourceBtn;
        PoolResourceListView poolResourceListView;

        PoolView selectPoolView;

        [MenuItem("Chipmunk/PoolEditor")]
        public static void OpenWindow()
        {
            PoolEditorWindow wnd = GetWindow<PoolEditorWindow>();
            wnd.titleContent = new GUIContent("PoolEditor");
        }

        public void CreateGUI()
        {
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/PoolEditor/Editor/PoolEditorWindow.uxml");

            VisualElement root = rootVisualElement;
            visualTreeAsset.CloneTree(root);

            QueryElement(root);
            Initialize();
        }

        private void QueryElement(VisualElement root)
        {
            inspectorView = root.Q<PoolInspectorView>();
            poolListView = root.Q<PoolListView>();
            poolCreateView = root.Q<PoolCreateView>();
            poolResourceListView = root.Q<PoolResourceListView>();
            objectField = root.Q<ObjectField>("FolderField");
            reloadResourceBtn = root.Q<Button>("ReloadResourceBtn");
        }

        private void Initialize()
        {
            poolListView.LoadView(GetPoolSOList());
            poolListView.onDelete += RemovePool;
            poolListView.onSelect += SelectPool;
            poolListView.onClickItem += item =>
            {
                inspectorView.Draw(item);
                Selection.activeObject = item;
            };
            poolListView.onClickItemDel += (item, poolSO) =>
            {
                poolSO.list.Remove(item);

                AssetDatabase.RemoveObjectFromAsset(item);
                AssetDatabase.SaveAssets();

                poolListView.LoadView(GetPoolSOList());
            };

            poolCreateView.onCreateBtnClick += CreatePool;
            poolResourceListView.LoadView(GetPoolAllResources());
            poolResourceListView.onClickViewBtn += AddPoolableToPool;
            poolResourceListView.onClickView += pref =>
            {
                Selection.activeObject = pref.ObjectPref;
            };

            reloadResourceBtn.RegisterCallback<ClickEvent>(evt =>
            {
                poolResourceListView.LoadView(GetPoolResources(objectField.value as DefaultAsset));
            });

            inspectorView.onDataChange += () => poolListView.LoadView(GetPoolSOList());
        }

        private void AddPoolableToPool(IPoolAble able)
        {
            if (selectPoolView.poolSO == null) return;
            foreach (PoolItemSO poolItem in selectPoolView.poolSO.list)
            {
                if (poolItem.prefab == able.ObjectPref)
                    return;
            }

            PoolItemSO poolItemSO = ScriptableObject.CreateInstance<PoolItemSO>();
            poolItemSO.prefab = able.ObjectPref;
            poolItemSO.poolName = able.PoolName;
            poolItemSO.name = "PoolItem";

            AssetDatabase.AddObjectToAsset(poolItemSO, selectPoolView.poolSO);
            AssetDatabase.SaveAssets();

            selectPoolView.poolSO.list.Add(poolItemSO);
            EditorUtility.SetDirty(selectPoolView.poolSO);
        }
        #region poolRegion
        private void SelectPool(PoolView view)
        {
            if (selectPoolView != null)
                selectPoolView.RemoveFromClassList("OnSelected");
            this.selectPoolView = view;
            selectPoolView.AddToClassList("OnSelected");

            inspectorView.Draw<PoolSO>(selectPoolView.poolSO);
            Selection.activeObject = selectPoolView.poolSO;
        }

        private void CreatePool(string name)
        {
            if (name == null || name == "")
            {
                poolListView.LoadView(GetPoolSOList());
                return;
            }

            string path = $"Assets/PoolEditor/ScriptableObejct/{name}";

            PoolSO poolSO = ScriptableObject.CreateInstance<PoolSO>();
            poolSO.poolName = name;
            PoolSO finedSO = AssetDatabase.LoadAssetAtPath<PoolSO>($"{path}.asset");
            int count = 0;
            if (finedSO == null)
            {
                CreatePoolAsset(poolSO, $"{path}.asset");
                return;
            }
            else
            {
                while (finedSO != null)
                {
                    count++;
                    finedSO = AssetDatabase.LoadAssetAtPath<PoolSO>($"{path} {count}.asset");
                }
            }
            CreatePoolAsset(poolSO, $"{path} {count}.asset");
        }
        private void CreatePoolAsset(PoolSO poolSO, string path)
        {
            AssetDatabase.CreateAsset(poolSO, path);
            AssetDatabase.SaveAssets();

            poolListView.LoadView(GetPoolSOList());
        }
        private void RemovePool(PoolSO poolSO)
        {
            // AssetDatabase.RemoveObjectFromAsset(poolSO);
            Undo.DestroyObjectImmediate(poolSO);
            AssetDatabase.SaveAssets();
        }
        public List<PoolSO> GetPoolSOList()
        {
            List<PoolSO> poolSOList = new();
            AssetDatabase.FindAssets("", new[] { "Assets/PoolEditor/ScriptableObejct" }).ToList().ForEach(guid =>
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                PoolSO poolSO = AssetDatabase.LoadAssetAtPath<PoolSO>(path);
                poolSOList.Add(poolSO);
            });

            return poolSOList;
        }
        #endregion
        #region poolResourRegion
        private List<IPoolAble> GetPoolResources(DefaultAsset defaultAsset)
        {
            if (defaultAsset == null)
                return GetPoolAllResources();

            List<IPoolAble> poolAbles = new();
            string path = AssetDatabase.GetAssetPath(defaultAsset);

            AssetDatabase.FindAssets("", new[] { path }).ToList().ForEach(guid =>
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (gameObject != null)
                {
                    IPoolAble poolAble = gameObject.GetComponent<IPoolAble>();

                    if (poolAble != null)
                        poolAbles.Add(poolAble);
                }
            });

            return poolAbles;
        }
        private List<IPoolAble> GetPoolAllResources()
        {
            List<IPoolAble> poolAbleList = new();

            string filter = "t:Prefab";
            string[] guids = AssetDatabase.FindAssets(filter);

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                if (prefab != null)
                {
                    if (prefab.TryGetComponent<IPoolAble>(out IPoolAble poolAble))
                    {
                        poolAbleList.Add(poolAble);
                    }
                }
            }

            return poolAbleList;
        }
        #endregion
    }
}