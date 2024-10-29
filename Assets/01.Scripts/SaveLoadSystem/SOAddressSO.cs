using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAddressSO", menuName = "ScriptableObjects/SOAddressSO", order = 1)]
public class SOAddressSO : Chipmunk.Library.ScriptableSingleton<SOAddressSO>
{
    public SerializableDictionary<uint, ScriptableObject> assetPaths;
    public ScriptableObject GetSOByID(uint id)
    {
        return assetPaths[id];
    }
    public uint GetIDBySO(ScriptableObject so)
    {
        foreach (KeyValuePair<uint, ScriptableObject> keyValuePair in assetPaths)
        {
            if (keyValuePair.Value == so)
            {
                return keyValuePair.Key;
            }
        }
        throw new System.Exception("This ScriptableObject is NotResistered");
    }
    protected override void OnEnable()
    {
        base.OnEnable();
#if UNITY_EDITOR
        SetAssetAddress();
#endif
    }
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
            Debug.Log(i + targetSO.name);
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
