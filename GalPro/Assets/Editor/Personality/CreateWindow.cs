using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateWindow : EditorWindow
{
    public int type;
    public string name;
    public int pos;
    public string talk;
    public AudioClip bgm;
    public AudioClip voice;
    public AudioClip effectAudio;
    public Texture2D img;
    public TextAsset scenario;

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
                //AddAssets(isAssets);
                ShowAssetGUI();

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
                ShowAssetGUI();
                //var unused = pos == -1 ? AddAssets(isAssets) : AddAssets(isAssets, true);

                //if (pos == -1)
                //{
                //    AddAssets(isAssets, true);
                //}
                //else
                //{
                //    AddAssets(isAssets);
                //}

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
        ShowButton("添加下一条");
    }



    void ShowButton(string message)
    {
        if (GUILayout.Button(message, GUILayout.Width(510)))
        {
            Debug.Log("添加或修改");
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
                    EditState.datas.Add(sceneData);
                    break;
                case 1:
                    BaseData chData = new CharacterItem();
                    chData.type = type;
                    chData.name = name;
                    chData.pos = pos;
                    chData.talk = talk;
                    if (isAssets)
                    {
                        if (pos == -1)
                        {
                            chData.assets = AddAssets(isAssets);
                        }
                        else
                        {
                            chData.assets = AddAssets(isAssets, true);
                        }

                    }
                    EditState.datas.Add(chData);
                    break;
                case 2:
                    BaseData snData = new SceneInfo();
                    snData.type = type;
                    snData.assets.scenario = SetPath("scenarios/", scenario);

                    EditState.datas.Add(snData);
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

    void ShowAssetGUI()
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
    }

    AssetsInfo AddAssets(bool isAsset, bool isCharacter = false)
    {
        AssetsInfo assetsInfo = new AssetsInfo();
        if (isCharacter)
        {
            assetsInfo.img = SetPath("character/", img);
        }
        else
        {
            assetsInfo.img = SetPath("sprites/", img);
        }

        assetsInfo.bgm = SetPath("audios/bgm/", bgm);
        assetsInfo.voice = SetPath("audios/voice/", voice);
        assetsInfo.effectVoice = SetPath("audios/effectVoice/", effectAudio);

        //if (type == 2)
        //{
        //    assetsInfo.scenario.name = scenario.name;
        //} 
        return assetsInfo;
    }

    Asset SetPath(string folders, Object obj)
    {
        Asset asset = new Asset();
        if (obj == null)
        {
            asset.name = "";
            asset.path = "";
        }
        else
        {
            asset.name = obj.name;
            asset.path = folders + obj.name + ".asset";
        }
        return asset;
    }
}
