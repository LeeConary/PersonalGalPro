public class FileData
{
    /// <summary>
    /// 读取类型。
    /// 0-加载背景，
    /// 1-加载人物剧本， 
    /// 2-加载脚本
    /// </summary>
    public string type;

    /// <summary>
    /// 人物名字
    /// </summary>
    public string name;

    /// <summary>
    /// 对话内容
    /// </summary>
    public string talk;

    /// <summary>
    /// 人物位置。
    /// 0-左，
    /// 1-中，
    /// 2-右，
    /// -1-莫得人物
    /// </summary>
    public string pos;

    /// <summary>
    /// 选项
    /// </summary>
    public string selections;

    /// <summary>
    /// 背景音乐
    /// </summary>
    public string bgm;

    /// <summary>
    /// 人物配音
    /// </summary>
    public string voice;

    /// <summary>
    /// 效果音
    /// </summary>
    public string effectVoice;

    /// <summary>
    /// 图像
    /// </summary>
    public string img;

    /// <summary>
    /// 剧本
    /// </summary>
    public string scenario;
}
