using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private Text ch_name;
    private Text ch_talk;
    private Image bg_pic;
    private Image[] characters;
    private GameObject textField;
    private GameObject ControlUI;

    private AudioSource bgm;
    private AudioSource effectVoice;
    private AudioSource voice;

    private bool isCompleteStr = true;

    private CharacterInfo info;
    private BaseData dataInfo;

    [SerializeField]
    private TextAsset text;

    private void Awake()
    { 
        textField = UtilMethods.GetOb("--USER INTERFACES", "textField");
        ControlUI = UtilMethods.GetOb("--USER INTERFACES", "ControlUI");

        ch_name = UtilMethods<Text>.GetComponent(textField, "ch_name");
        ch_talk = UtilMethods<Text>.GetComponent(textField, "ch_talk");

        characters = UtilMethods<Image>.GetComponentsInChildren("--IMAGES", "characters");
        bg_pic = UtilMethods<Image>.GetComponent("--IMAGES", "bg_pic");

        bgm = UtilMethods<AudioSource>.GetComponent("--VOICES", "bgm");
        effectVoice = UtilMethods<AudioSource>.GetComponent("--VOICES", "effectVoice");
        voice = UtilMethods<AudioSource>.GetComponent("--VOICES", "voice");

    }
    // Start is called before the first frame update
    void Start()
    {
        if (LoadScenarioData.Instance.LoadScript(text))
        {
            dataInfo = LoadScenarioData.Instance.GetNextScenario();
            HandleData(dataInfo);
        }
        
    }
    public void OnClick()
    {
        if (!isCompleteStr)
        {
            StopCoroutine("TextAppear");
            ch_talk.text = dataInfo.talk;
            isCompleteStr = true;
            return;
        }
        else
        {
            dataInfo = LoadScenarioData.Instance.GetNextScenario();
            HandleData(dataInfo);
            isCompleteStr = false;
        }
        StartCoroutine("TextAppear", 0.05f);
        //HandleData(info);
    }

    IEnumerator TextAppear(float duration)
    {
        if (dataInfo.talk == "" || dataInfo.talk == null)
        {
            StopCoroutine("TextAppear");
            isCompleteStr = true;
            yield return null;
        }
        else
        {
            for (int i = 0; i <= dataInfo.talk.Length; i++)
            {
                ch_talk.text = dataInfo.talk.Substring(0, i);
                yield return new WaitForSeconds(duration);
                if (i >= dataInfo.talk.Length)
                {
                    isCompleteStr = true;
                }
            }
        }
        
    }

    Sprite LoadPic(string path)
    {
        FileStream fstream = new FileStream(path, FileMode.Open);
        fstream.Seek(0, SeekOrigin.Begin);
        byte[] bytes = new byte[fstream.Length];
        fstream.Read(bytes, 0, (int)fstream.Length);
        fstream.Close();
        fstream.Dispose();
        fstream = null;

        int width = Screen.width;
        int height = Screen.height;
        Texture2D texture2D = new Texture2D(width, height);
        texture2D.LoadImage(bytes);

        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5F, 0.5F));
        return sprite;
    }

    void setImage(Image image, string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("图片资源加载出错");
            return;
        }
        Sprite sprite = LoadPic(path);
        image.sprite = sprite;
    }

    void setText(Text text, string content)
    {
        text.text = content;
    }

    void setVoices(AudioSource source, string assetPath, bool isLoop = false, int loopTime = 0)
    {
        if (string.IsNullOrEmpty(assetPath))
        {
            return;
        }
        AudioClip clip = Resources.Load(assetPath) as AudioClip;
        source.clip = clip;
        if (isLoop)
        {
            source.loop = true;
            source.Play();
        }
        else
        {
            source.Play();
        }
    }

    void HandleData(CharacterInfo _info)
    {
        try
        {
            if (_info == null)
            {
                return;
            }
            if (_info.type == 0)
            {
                if (_info.name == "")
                {
                    setImage(bg_pic, _info.assetPath);
                    StartCoroutine(SwitchLoad());
                }
                else
                {
                    textField.SetActive(true);
                    setText(ch_name, _info.name);
                    setText(ch_talk, _info.talk);
                }
            }
            else if (_info.type == 2)
            {
                LoadScenarioData.Instance.LoadScript(_info.assetPath);
                HandleData(info);
            }
            else
            {
                int pos = int.Parse(_info.pos);
                textField.SetActive(true);
                switch (pos)
                {
                    case 0:
                        characters[0].gameObject.SetActive(true);
                        setImage(characters[0], _info.assetPath);
                        characters[1].gameObject.SetActive(false);
                        characters[2].gameObject.SetActive(false);
                        break;
                    case 1:
                        characters[1].gameObject.SetActive(true);
                        setImage(characters[1], _info.assetPath);
                        characters[0].gameObject.SetActive(false);
                        characters[2].gameObject.SetActive(false);
                        break;
                    case 2:
                        characters[2].gameObject.SetActive(true);
                        setImage(characters[2], _info.assetPath);
                        characters[0].gameObject.SetActive(false);
                        characters[1].gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                setText(ch_name, _info.name);
                setText(ch_talk, _info.talk);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("下一场景");
        }
    }

    void HandleData(BaseData _info)
    {
        try
        {
            if (_info == null)
            {
                return;
            }
            if (_info.type == 0)
            {
                if (string.IsNullOrEmpty(_info.talk))
                {
                    setImage(bg_pic, _info.assets.img);
                    setVoices(bgm, _info.assets.bgm);
                    bg_pic.color = new Color(bg_pic.color.r, bg_pic.color.g, bg_pic.color.b, 0);
                    bg_pic.DOFade(1, 1f);
                    StartCoroutine(SwitchLoad());
                }
            }
            else if (_info.type == 2)
            {
                characters[2].gameObject.SetActive(false);
                characters[0].gameObject.SetActive(false);
                characters[1].gameObject.SetActive(false);
                textField.SetActive(false);
                bg_pic.DOFade(0, 1f);
                if (LoadScenarioData.Instance.LoadScript(_info.assets.scenario))
                {
                    HandleData(dataInfo);
                }
            }
            else
            {
                setVoices(bgm, _info.assets.bgm);
                setVoices(bgm, _info.assets.effectVoice);
                setVoices(bgm, _info.assets.voice);

                textField.SetActive(true);
                if (_info.pos == -1)
                {
                    setText(ch_name, _info.name);
                    setText(ch_talk, _info.talk);
                }
                else
                {
                    switch (_info.pos)
                    {
                        case 0:
                            characters[0].gameObject.SetActive(true);
                            setImage(characters[0], _info.assets.img);
                            characters[1].gameObject.SetActive(false);
                            characters[2].gameObject.SetActive(false);
                            break;
                        case 1:
                            characters[1].gameObject.SetActive(true);
                            setImage(characters[1], _info.assets.img);
                            characters[0].gameObject.SetActive(false);
                            characters[2].gameObject.SetActive(false);
                            break;
                        case 2:
                            characters[2].gameObject.SetActive(true);
                            setImage(characters[2], _info.assets.img);
                            characters[0].gameObject.SetActive(false);
                            characters[1].gameObject.SetActive(false);
                            break;
                        default:
                            break;
                    }
                }
                setText(ch_name, _info.name);
                setText(ch_talk, _info.talk);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("下一场景");
        }
    }
    IEnumerator SwitchLoad()
    {
        characters[2].gameObject.SetActive(false);
        characters[0].gameObject.SetActive(false);
        characters[1].gameObject.SetActive(false);
        textField.SetActive(false);
        ControlUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        ControlUI.SetActive(true);
    }
}
