using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
#if UNITY_EDITOR
public class SaveWindow : EditorWindow
{
    string txtName;

    private void OnGUI()
    {
        GUILayout.BeginVertical("Box");
        txtName = EditorGUILayout.TextField("脚本名字", txtName);
        if (GUILayout.Button("保存", GUILayout.Width(200)))
        {
            //保存逻辑
            string datas = UtilMethods<List<BaseData>>.EntityToJson(EditState.datas);
            string path = Application.dataPath + "/Resources/Scenarios/" + txtName + ".json";

            if (!File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
            }
            File.WriteAllText(path, datas);

            EditState.lastEditScenarioPath = path;

            if (!string.IsNullOrEmpty(EditState.lastEditScenarioPath))
            {
                if (!File.Exists(EditState.editSaveData))
                {
                    FileStream fs = new FileStream(EditState.editSaveData, FileMode.Create, FileAccess.ReadWrite);
                    fs.Close();
                }
                File.WriteAllText(EditState.editSaveData, EditState.lastEditScenarioPath);
            }

            AssetDatabase.Refresh();
        }
    }
}
#endif

