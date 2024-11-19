using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EntityEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Chipmunk/EntityEditor")]
    public static void ShowExample()
    {
        EntityEditorWindow wnd = GetWindow<EntityEditorWindow>();
        wnd.titleContent = new GUIContent("EntityEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        m_VisualTreeAsset.CloneTree(root);  
    }
}
