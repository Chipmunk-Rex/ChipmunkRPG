using System;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library.PoolEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolEditorWindow : EditorWindow
    {
        PoolInspectorView inspectorView;
        PoolListView poolListView;
        PoolCreateView poolCreateView;

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
        }

        private void Initialize()
        {
            poolListView.LoadView(GetPoolSOList());
            poolListView.onDelete += RemovePool;
            poolListView.onSelect += SelectPool;

            poolCreateView.onCreateBtnClick += CreatePool;

            inspectorView.onDataChange += () => poolListView.LoadView(GetPoolSOList());
        }

        private void SelectPool(PoolView view)
        {
            inspectorView.Draw<PoolSO>(view.poolSO);
            Selection.activeObject = view.poolSO;
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
            Debug.Log(poolSOList.Count);

            return poolSOList;
        }
    }
}