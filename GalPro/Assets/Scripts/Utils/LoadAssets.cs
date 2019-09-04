using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssets : MonoBehaviour
{
    public static LoadAssets Instance;

    private AssetBundle assetBundle;
    UnityEngine.Object[] objects;
    private void Awake()
    {
        Instance = this;
    }
    IEnumerator LoadBundle(string path)
    {
#if UNITY_EIDTOR || UNITY_STANDALONE_WIN
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(Path.Combine(Application.dataPath, path));
#elif UNTIY_ANDRIOD
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(Path.Combine(Application.streamingAssetsPath, name));
#endif
        yield return request.SendWebRequest();
        assetBundle = DownloadHandlerAssetBundle.GetContent(request);
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("读取" + path + "完成");
            objects = assetBundle.LoadAllAssets();
        }
    }

    public UnityEngine.Object[] LoadPackgedAssets(string path)
    {
        //StartCoroutine("LoadBundle", path);
        UnityEngine.Object[] objects = LoadAndGetBundleAssets(path);
        return objects;
    }

    public UnityEngine.Object[] LoadAndGetBundleAssets(string path, Type type = null)
    {
#if UNITY_EIDTOR || UNITY_STANDALONE_WIN
        string realPath = Application.dataPath + "/AssetBundles/" + path;
#elif UNTIY_ANDRIOD
       string realPath = Path.Combine(Application.streamingAssetsPath, path);
#endif
        WWW www = new WWW(realPath);
        www = WWW.LoadFromCacheOrDownload(realPath, 0);
        AssetBundle bundles = www.assetBundle;
        
        UnityEngine.Object[] objects = bundles.LoadAllAssets(type);
        Debug.Log("读取完成");
        return objects;
    }
}
