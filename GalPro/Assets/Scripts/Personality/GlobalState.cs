using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalState
{
    public static List<BaseData> datas;
    public static string currentScenario;
    public static int index;

    public static string lastEditScenarioPath;
    public static readonly string editSaveData = Application.dataPath + "/editsave.txt";

    public struct saveData
    {
        public static int index;
        public static string path;
    }
}

