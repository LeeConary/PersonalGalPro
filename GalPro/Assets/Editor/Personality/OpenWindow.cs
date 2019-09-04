using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OpenWindow : EditorWindow
{
    List<BaseData> list;
    Vector2 scrollPos;
    public TextAsset openScenario;

    bool foldout;
    private void OnGUI()
    {
        openScenario = EditorGUILayout.ObjectField("剧本文件", openScenario, typeof(TextAsset), true) as TextAsset;
        if (openScenario)
        {
            list = UtilMethods<BaseData>.JsonToEntity(AssetDatabase.GetAssetPath(openScenario));
            //foldout = new bool[list.Count];

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            for (int i = 0; i < list.Count; i++)
            {
                RenderInfoWindow(list[i], i);
            }
            EditorGUILayout.EndScrollView();

            EditState.datas = list;
        }
    }
    
    void RenderInfoWindow(BaseData data, int index)
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("type：" + data.type.ToString() ?? "");
        GUILayout.Label("name： " + data.name.ToString() ?? "");
        GUILayout.Label("pos： " + data.pos.ToString() ?? "");
        GUILayout.Label("talk：" + (data.talk ?? "").ToString());  
        foldout = GUILayout.Toggle(foldout, "Assets");
        if (foldout)
        {
            RenderAssetInfo("img", data.assets.img);
            RenderAssetInfo("bgm", data.assets.bgm);
            RenderAssetInfo("voice", data.assets.voice);
            RenderAssetInfo("effectVoice", data.assets.effectVoice);
            RenderAssetInfo("scenario", data.assets.scenario);
        }    

        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("修改"))
        {
            Debug.Log("修改");
            Rect rect = new Rect(50, 50, 520, 500);
            CreateWindow window = (CreateWindow)GetWindowWithRect(typeof(CreateWindow), rect, true);
        }
        if (GUILayout.Button("删除"))
        {
            Debug.Log("删除");
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    void RenderAssetInfo(string name, Asset asset)
    {
        GUILayout.Label(name);
        GUILayout.BeginVertical("Box");
        if (asset == null)
        {
            GUILayout.Label("name：");
            GUILayout.Label("path：");
        }
        else
        {
            GUILayout.Label("name：" + asset.name);
            GUILayout.Label("path：" + asset.path);
        }
        GUILayout.EndVertical();

    }
}
