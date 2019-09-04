 using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class MainWindow : EditorWindow
{
    [MenuItem("GalTool/EditWindow")]
    static void OpenMainWin()
    {
        Rect rect = new Rect(0, 0, 520, 120);
        MainWindow window = (MainWindow)GetWindowWithRect(typeof(MainWindow), rect, true);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("新建脚本", GUILayout.Width(250), GUILayout.Height(50)))
        {
            //新建脚本
            Debug.Log("新建脚本");
            Rect rect = new Rect(50, 50, 520, 500);
            CreateWindow window = (CreateWindow)GetWindowWithRect(typeof(CreateWindow), rect, true);

            EditState.datas = new List<BaseData>();
        }
        if (GUILayout.Button("打开上次编辑的脚本", GUILayout.Width(250), GUILayout.Height(50)))
        {
            //打开脚本
            Debug.Log("打开脚本");
            string path = null;
            if (File.Exists(EditState.editSaveData))
            {
                StreamReader sr = new StreamReader(EditState.editSaveData);
                while (!sr.EndOfStream)
                {
                     path = sr.ReadLine();
                }
                sr.Dispose();
                sr.Close();

                OpenWindow window = (OpenWindow)GetWindow(typeof(OpenWindow), true);
                window.openScenario = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
                window.Show();
            }
            else
            {
                Debug.LogError("未找到存档文件");
            }
            
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("保存脚本", GUILayout.Width(250), GUILayout.Height(50)))
        {
            //保存脚本
            Debug.Log("保存脚本");

            SaveWindow window = (SaveWindow)GetWindowWithRect(typeof(SaveWindow),
                new Rect(0, 0, 500, 50), true);
            window.Show();
        }

        if (GUILayout.Button("查看脚本", GUILayout.Width(250), GUILayout.Height(50)))
        {
            //查看脚本
            Debug.Log("查看脚本");
            OpenWindow window = (OpenWindow)GetWindow(typeof(OpenWindow),true);
            window.Show();

        }
        GUILayout.EndHorizontal();
    }
}
#endif