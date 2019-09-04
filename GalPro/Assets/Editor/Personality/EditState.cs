using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EditState
{
    public static List<BaseData> datas;
    public static string currentScenario;
    public static int editIndex;

    public static string lastEditScenarioPath;
    public static readonly string editSaveData = Application.dataPath + "/editsave.txt";

    public static EditType editType;
    public enum EditType
    {
        Add,
        Edit
    }

    public struct saveData
    {
        public static int index;
        public static string path;
    }
}
