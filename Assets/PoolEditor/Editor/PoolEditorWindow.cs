using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset visualTreeAsset = default;

    [MenuItem("Chipmunk/PoolEditor")]
    public static void ShowExample()
    {
        PoolEditorWindow wnd = GetWindow<PoolEditorWindow>();
        wnd.titleContent = new GUIContent("PoolEditorWindow");
    }

    public void CreateGUI()
    {
        visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/PoolEditor/Editor/PoolEditorWindow.uxml");

        VisualElement root = rootVisualElement;
        visualTreeAsset.CloneTree(root);
    }
}
