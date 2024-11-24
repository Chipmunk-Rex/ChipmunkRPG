using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftRecipeEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Chipmunk/CraftRecipeEditor")]
    public static void OepnWindow()
    {
        CraftRecipeEditorWindow wnd = GetWindow<CraftRecipeEditorWindow>();
        wnd.titleContent = new GUIContent("CraftRecipeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        m_VisualTreeAsset.CloneTree(root);
        
    }
}
