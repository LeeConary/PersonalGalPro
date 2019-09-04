using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetLoad : MonoBehaviour
{
    [MenuItem("GalTool/Build AssetsBundles")]
    static void BuildAllAssetBundles()
    {
#if UNITY_EIDTOR || UNITY_STANDALONE_WIN
        string dir = Application.dataPath + "/AssetBundles";
#elif UNITY_ANDRIOD
        string dir = Application.streamingAssetsPath + "/AssetBundles";
#endif

        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        AssetDatabase.Refresh();
    }
}
