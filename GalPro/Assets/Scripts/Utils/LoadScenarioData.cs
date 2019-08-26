#define json

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class LoadScenarioData:MonoBehaviour
{
    public struct LoadStr
    {

        public int type;
        public string pos;
        public string name;
        public string talk;
        public string assetPath;
    }
    #region 单例
    public static LoadScenarioData Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    int index = 0;
    List<string> txt;
    List<CharacterInfo> infoList;

    List<BaseData> fDatas;
    public bool LoadScript(TextAsset path)
    {
#if txt
        index = 0;
        txt = new List<string>();
        StreamReader stream = new StreamReader(path);
        while (!stream.EndOfStream)
        {
            txt.Add(stream.ReadLine());
        }
        stream.Close();
#elif json
        #region 读取json文件
        fDatas = UtilMethods<BaseData>.JsonToEntity(path.text, true);
        return true;
        #endregion
#elif test

#endif
    }

    public bool LoadScript(string path)
    {
#if txt
        index = 0;
        txt = new List<string>();
        StreamReader stream = new StreamReader(path);
        while (!stream.EndOfStream)
        {
            txt.Add(stream.ReadLine());
        }
        stream.Close();
#elif json
        #region 读取json文件
        if (!File.Exists(path))
        {
            Debug.LogError("未找到剧本资源");
            return false;
        }
        fDatas = UtilMethods<BaseData>.JsonToEntity(path);
        return true;
        #endregion
#elif test

#endif
    }

    public BaseData GetNextScenario()
    {
        #region 读取json文件
        if (index < fDatas.Count)
        {
            int type = fDatas[index].type;
            switch (type)
            {
                case 0:
                    BaseData info1 = new SceneInfo();
                    info1 = fDatas[index];
                    index++;
                    return info1;
                case 1:
                    BaseData info2 = new CharacterItem();
                    info2 = fDatas[index];
                    index++;
                    return info2;
                case 2:
                    BaseData info3 = new SceneInfo();
                    info3 = fDatas[index];
                    index = 0;
                    return info3;
                default:
                    break;
            }
        }
        return null;
        #endregion

    }

    public CharacterInfo GetNext()
    {
#if txt
        #region 读取txt文件
        if (index < txt.Count)
        {
            LoadStr load = GetLoadStr(txt[index].Split(','));
            int type = load.type;
            switch (type)
            {
                case 0:
                    index++;
                    return new CharacterInfo(type, load.assetPath);
                    break;
                case 1:
                    string pos = load.pos;
                    string name = load.name;
                    string talk = load.talk;
                    string picname = load.assetPath;
                    index++;
                    return new CharacterInfo(type, pos, name, talk, picname);
                    break;
                case 2:
                    index = 0;
                    return new CharacterInfo(type, load.assetPath);
                    break;
                default:
                    return null;
                    break;
            }
        }
        else
        {
            return null;
        }
        #endregion
#elif test
        #region 读取json文件
        if (index < infoList.Count)
        {
            int type = infoList[index].type;
            CharacterInfo info = new CharacterInfo();
            switch (type)
            {
                case 2:
                    info = new CharacterInfo(type, infoList[index].assetPath);
                    index = 0;
                    return info;
                    break;
                default:
                    info = infoList[index];
                    index++;
                    return info;
                    break;
            }
        }
        return null;
        #endregion
#elif json
        return null;
#endif
    }
}
