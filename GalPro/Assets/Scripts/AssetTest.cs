using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;

public class AssetTest : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundle From Selection  -- Track dependencies")]
    static void ExportAssetBundle()
    {
        string path = "Assets/myAssetBundle.unity3d";
        if (path.Length != 0)
        {
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows);
            Selection.objects = selection;
        }
    }

    [MenuItem("Assets/Build AssetBundle From Selection -- No dependency tracking")]
    static void ExportResourceNoTrack()
    {
        string path = "Assets/myAssetBundle.unity3d";
        if (path.Length != 0)
        {
            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows);
        }
    }

    public string name;
    public int version;
    public IEnumerator LoadBundle()
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(Path.Combine(Application.streamingAssetsPath , name));
        yield return request.SendWebRequest();
        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);

        Texture2D texture2D = assetBundle.LoadAsset("60702 #277327", typeof(Texture2D)) as Texture2D;

        AudioClip audioClip = assetBundle.LoadAsset("1", typeof(AudioClip)) as AudioClip;

        //ob.AddComponent<SpriteRenderer>().sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        AudioSource source = new GameObject().AddComponent<AudioSource>();
        source.clip = audioClip;
        source.Play();
    }

    private void Start()
    {
        StartCoroutine(LoadBundle());
    }
}
