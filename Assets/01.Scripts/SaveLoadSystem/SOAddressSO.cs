using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAddressSO", menuName = "ScriptableObjects/SOAddressSO", order = 1)]
public class SOAddressSO : Chipmunk.Library.ScriptableSingleton<SOAddressSO>
{
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EditorInitialize()
    {
        Instance.SetAssetAddress();
    }
#endif
    public SerializableDictionary<uint, ScriptableObject> assetPaths;
    public ScriptableObject GetSOByID(uint id)
    {
        return assetPaths[id];
    }
    public T GetSOByID<T>(uint id) where T : ScriptableObject
    {
        return assetPaths[id] as T;
    }
    public uint GetIDBySO(ScriptableObject so)
    {
        if (so == null)
        {
            throw new System.Exception("This ScriptableObject is Null");
        }
        foreach (KeyValuePair<uint, ScriptableObject> keyValuePair in assetPaths)
        {
            if (keyValuePair.Value == so)
            {
                return keyValuePair.Key;
            }
        }
        throw new System.Exception("This ScriptableObject is NotResistered");
    }
//     protected override void OnEnable()
//     {
//         base.OnEnable();
// #if UNITY_EDITOR
//         SetAssetAddress();
// #endif
//     }
#if UNITY_EDITOR
    [ContextMenu("SetAssetAddress")]
    public void SetAssetAddress()
    {
        assetPaths.Clear();
        List<ScriptableObject> scriptableObjects = GetAllScriptableObjects();
        for (uint i = 0; i < scriptableObjects.Count; i++)
        {
            ScriptableObject targetSO = scriptableObjects[(int)i] as ScriptableObject;
            assetPaths.Add(i, targetSO);
        }

        EditorUtility.SetDirty(this);
    }

    public List<ScriptableObject> GetAllScriptableObjects()
    {
        List<ScriptableObject> allScriptableObjects = new List<ScriptableObject>();
        string[] assetPaths = AssetDatabase.GetAllAssetPaths();

        foreach (string path in assetPaths)
        {
            if (path.StartsWith("Assets") && path.EndsWith(".asset"))
            {
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (so != null)
                {
                    allScriptableObjects.Add(so);
                }
            }
        }

        return allScriptableObjects;
    }

#endif
}
