using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class EditWindow : EditorWindow
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
    }
    void InitDatas(int editIndex)
    {
        BaseData info = EditState.datas[editIndex];
        type = info.type;
        switch (type)
        {
            case -1:
                return;
            case 0:
                name = info.name;
                pos = info.pos;
                break;
            case 1:
                name = info.name;
                pos = info.pos;
                talk = info.talk;
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
    }
}
#endif