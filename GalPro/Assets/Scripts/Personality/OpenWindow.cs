using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class OpenWindow : EditorWindow
{
    List<BaseData> list;
    Vector2 scrollPos;
    public TextAsset openScenario;
    private void OnGUI()
    {
        openScenario = EditorGUILayout.ObjectField("剧本文件", openScenario, typeof(TextAsset), true) as TextAsset;
        if (openScenario)
        {
            list = UtilMethods<BaseData>.JsonToEntity(AssetDatabase.GetAssetPath(openScenario));
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            for (int i = 0; i < list.Count; i++)
            {
                RenderInfoWindow(list[i]);
            }
            EditorGUILayout.EndScrollView();

            GlobalState.datas = list;
        }
    }

    void RenderInfoWindow(BaseData data)
    {
        int type;
        string name;
        int pos;
        string talk;
        AudioClip bgm;
        AudioClip voice;
        AudioClip effectAudio;
        Texture2D img;
        TextAsset scenario;

        GUILayout.BeginVertical("Box");
        GUILayout.Label("type：" + data.type.ToString() ?? "");
        GUILayout.Label("name： " + data.name.ToString() ?? "");
        GUILayout.Label("pos： " + data.pos.ToString() ?? "");
        GUILayout.Label("talk：" + (data.talk ?? "").ToString());
        GUILayout.Label("img： " + data.assets.img ?? "".ToString());
        GUILayout.Label("bgm： " + data.assets.bgm ?? "".ToString());
        GUILayout.Label("voice： " + data.assets.voice ?? "".ToString());
        GUILayout.Label("effectVoice： " + data.assets.effectVoice ?? "".ToString());
        GUILayout.Label("scenario： " + data.assets.scenario ?? "".ToString());
        GUILayout.EndVertical();
    }
}
#endif
