using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolSO : ScriptableObject
{
    public string poolName;
    [HideInInspector] private string beforeName = null;
    [HideInInspector] public List<PoolItemSO> list = new();

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (beforeName == null)
            beforeName = poolName;

        if (poolName == null || poolName == "")
            return;

        string assetPath = AssetDatabase.GetAssetPath(this);
        string fileName = System.IO.Path.GetFileNameWithoutExtension(assetPath);

        if (fileName != poolName)
        {
            string path = System.IO.Path.GetDirectoryName(assetPath);
            PoolSO findPool = AssetDatabase.LoadAssetAtPath<PoolSO>($"{path}/{poolName}.asset");
            if (findPool != null)
            {
                poolName = beforeName;
                return;
            }
            try
            {
                AssetDatabase.RenameAsset(assetPath, poolName);
                AssetDatabase.SaveAssets();
            }
            catch
            {
                poolName = beforeName;
            }
            beforeName = poolName;
        }
    }
#endif
}
