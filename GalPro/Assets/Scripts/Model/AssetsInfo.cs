
public class AssetsInfo 
{
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

    public AssetsInfo()
    {
    }

    public AssetsInfo(string img, string bgm  , string voice , string effectVoice,string scenario)
    {
        this.img = img;
        this.bgm = bgm;
        this.voice = voice;
        this.effectVoice = effectVoice;
        this.scenario = scenario;
    }
}
