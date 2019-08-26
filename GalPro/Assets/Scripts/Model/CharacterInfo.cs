using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoList
{
    public List<CharacterInfo> list;
}

[Serializable]
public class CharacterInfo 
{
    /// <summary>
    /// 读取类型。
    /// 0-加载背景，
    /// 1-加载人物剧本， 
    /// 2-加载脚本
    /// </summary>
    public int type;

    /// <summary>
    /// 人物位置。
    /// 0-左，
    /// 1-中，
    /// 2-右，
    /// -1-莫得人物
    /// </summary>
    public string pos;

    /// <summary>
    /// 人物名字
    /// </summary>
    public string name;

    /// <summary>
    /// 人物对话内容
    /// </summary>
    public string talk;

    /// <summary>
    /// 资源路径
    /// </summary>
    public string assetPath;

    /// <summary>
    /// 选项
    /// </summary>
    public List<string> selections;

    public CharacterInfo(int type, string pos, string name, string talk, string assetPath)
    {
        this.type = type;
        this.pos = pos;
        this.name = name;
        this.talk = talk;
        this.assetPath = assetPath;
    }

    public CharacterInfo(int type, string assetPath)
    {
        this.type = type;
        this.assetPath = assetPath;
    }

    public CharacterInfo() { }
}
