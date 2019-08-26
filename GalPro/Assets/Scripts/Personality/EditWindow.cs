using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class EditWindow : EditorWindow
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

    bool isAssets = false;

    private void OnGUI()
    {
        type = EditorGUILayout.IntPopup("类型", type,
            new[] { "--请选择--", "加载场景", "加载人物对话 或 CG背景", "加载剧本文件" },
            new[] { -1, 0, 1, 2 });
        switch (type)
        {
            case -1:
                return;
            case 0:
                GUILayout.BeginVertical("Box");
                GUILayout.Label("加载场景");
                GUILayout.Label(" ");

                name = EditorGUILayout.TextField("名字：", name);
                pos = -1;

                isAssets = GUILayout.Toggle(isAssets, "添加媒体资源");
                AddAssets(isAssets);

                GUILayout.EndVertical();
                break;
            case 1:
                GUILayout.BeginVertical("Box");
                GUILayout.Label("加载人物对话 或 CG背景");
                GUILayout.Label(" ");

                name = EditorGUILayout.TextField("名字：", name);
                pos = EditorGUILayout.IntPopup("位置", pos,
                         new[] { "左", "中", "右", "莫得人物" },
                         new[] { 0, 1, 2, -1 });

                GUILayout.Label("内容：");
                talk = EditorGUILayout.TextArea(talk, GUILayout.Height(50));

                isAssets = GUILayout.Toggle(isAssets, "添加媒体资源");
                AddAssets(isAssets);

                GUILayout.EndVertical();

                break;
            case 2:
                GUILayout.BeginVertical("Box");
                GUILayout.Label("加载剧本文件");
                GUILayout.Label(" ");
                scenario = EditorGUILayout.ObjectField("剧本文件", scenario, typeof(TextAsset), true) as TextAsset;
                GUILayout.EndVertical();
                break;
        }

        if (GUILayout.Button("添加下一条", GUILayout.Width(510)))
        {
            Debug.Log("添加下一条");
            switch (type)
            {
                case -1:
                    return;
                case 0:
                    BaseData sceneData = new SceneInfo();
                    sceneData.type = type;
                    sceneData.name = name;
                    sceneData.pos = pos;
                    if (isAssets)
                    {
                        sceneData.assets = AddAssets(isAssets);
                    }
                    GlobalState.datas.Add(sceneData);
                    break;
                case 1:
                    BaseData chData = new CharacterItem();
                    chData.type = type;
                    chData.name = name;
                    chData.pos = pos;
                    chData.talk = talk;
                    if (isAssets)
                    {
                        chData.assets = AddAssets(isAssets);
                    }
                    GlobalState.datas.Add(chData);
                    break;
                case 2:
                    BaseData snData = new SceneInfo();
                    snData.type = type;
                    snData.assets.scenario = AssetDatabase.GetAssetPath(scenario);
                    GlobalState.datas.Add(snData);
                    break;
                default:
                    break;
            }

            type = -1;
            name = "";
            pos = -1;
            talk = "";
            bgm = null;
            voice = null;
            effectAudio = null;
            scenario = null;
            img = null;

            isAssets = false;
        }
    }

    AssetsInfo AddAssets(bool isAsset)
    {
        if (isAssets)
        {
            GUILayout.BeginVertical("Box");
            img = EditorGUILayout.ObjectField("图片", img, typeof(Texture2D), true) as Texture2D;
            bgm = EditorGUILayout.ObjectField("背景音乐", bgm, typeof(AudioClip), true) as AudioClip;
            voice = EditorGUILayout.ObjectField("人物配音", voice, typeof(AudioClip), true) as AudioClip;
            effectAudio = EditorGUILayout.ObjectField("效果音", effectAudio, typeof(AudioClip), true) as AudioClip;
            GUILayout.EndVertical();
        }

        AssetsInfo assetsInfo = new AssetsInfo();
        assetsInfo.img = AssetDatabase.GetAssetPath(img) ?? "";
        assetsInfo.bgm = GetPathString(bgm);
        assetsInfo.voice = GetPathString(voice);
        assetsInfo.effectVoice = GetPathString(effectAudio);
        assetsInfo.scenario = "";
        return assetsInfo;
    }

    string GetPathString(Object obj)
    {
        string str = AssetDatabase.GetAssetPath(obj) ?? "";
        string root = "Asset/Resources/";
        if (!string.IsNullOrEmpty(str))
        {
            str = str.Substring(root.Length + 1, str.Length -  (5 + root.Length));
        }
        return str;
    }
}
#endif