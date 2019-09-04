
public class BaseData 
{
    /// <summary>
    /// 读取类型。
    /// 0-加载背景，
    /// 1-加载人物剧本， 
    /// 2-加载脚本
    /// </summary>
    public int type;

    /// <summary>
    /// 人物名字
    /// </summary>
    public string name;

    /// <summary>
    /// 人物位置。
    /// 0-左，
    /// 1-中，
    /// 2-右，
    /// -1-莫得人物
    /// </summary>
    public int pos;

    /// <summary>
    /// 对话内容
    /// </summary>
    public string talk;

    /// <summary>
    /// 资源
    /// </summary>
    public AssetsInfo assets = new AssetsInfo();

    public BaseData(int type, string name, string talk, AssetsInfo assets)
    {
        this.type = type;
        this.name = name;
        this.talk = talk;
        this.assets = assets;
    }

    public BaseData()
    {
    }
}
