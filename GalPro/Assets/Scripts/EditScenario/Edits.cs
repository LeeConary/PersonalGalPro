using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Edits : MonoBehaviour
{
    [SerializeField]
    Dropdown dropdown;
    [SerializeField]
    GameObject CreateCharacter;
    [SerializeField]
    GameObject CreateBg;
    [SerializeField]
    GameObject CreateScenario;

    InputField name;
    InputField img;
    InputField effectVoice;
    InputField bgm;
    AssetsInfo assets;

    public void DropSelect()
    {
        switch (dropdown.value)
        {
            case 0:
                return;
            case 1:
                CreateBg.SetActive(true);
                CreateCharacter.SetActive(false);
                CreateScenario.SetActive(false);
                break;
            case 2:
                CreateBg.SetActive(false);
                CreateCharacter.SetActive(true);
                CreateScenario.SetActive(false);
                break;
            case 3:
                CreateBg.SetActive(false);
                CreateCharacter.SetActive(false);
                CreateScenario.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void AddContent()
    {
        
        switch (dropdown.value)
        {
            case 0:
                return;
            case 1:
                name = UtilMethods.GetUIOb(CreateBg, "name").GetComponentInChildren<InputField>();
                img = UtilMethods.GetUIOb(CreateBg, "img").GetComponentInChildren<InputField>();
                effectVoice = UtilMethods.GetUIOb(CreateBg, "effectVoice").GetComponentInChildren<InputField>();
                bgm = UtilMethods.GetUIOb(CreateBg, "bgm").GetComponentInChildren<InputField>();

                BaseData sc_data = new SceneInfo();
                sc_data.type = dropdown.value;
                sc_data.name = name.text;
                sc_data.pos = -1;

                assets = new AssetsInfo(img.text, bgm.text, "", effectVoice.text, "");
                sc_data.assets = assets;

                CreateManager.Instance.dataList.Add(sc_data);
                break;
            case 2:
                name = UtilMethods.GetUIOb(CreateCharacter, "name").GetComponentInChildren<InputField>();
                Dropdown pos = UtilMethods.GetUIOb(CreateCharacter, "pos").GetComponentInChildren<Dropdown>();
                InputField content = UtilMethods.GetUIOb(CreateCharacter, "content").GetComponentInChildren<InputField>();
                img = UtilMethods.GetUIOb(CreateCharacter, "img").GetComponentInChildren<InputField>();
                effectVoice = UtilMethods.GetUIOb(CreateCharacter, "effectVoice").GetComponentInChildren<InputField>();
                bgm = UtilMethods.GetUIOb(CreateCharacter, "bgm").GetComponentInChildren<InputField>();

                BaseData ch_data = new CharacterItem();
                ch_data.type = dropdown.value;
                ch_data.name = name.text;
                ch_data.pos = pos.value >= 4 ? -1 : pos.value;
                ch_data.talk = content.text;

                assets = new AssetsInfo(img.text, bgm.text, "", effectVoice.text, "");
                ch_data.assets = assets;

                CreateManager.Instance.dataList.Add(ch_data);
                break;
            case 3:
                InputField senario = UtilMethods.GetUIOb(CreateScenario, "senario").GetComponentInChildren<InputField>();
                BaseData se_data = new SceneInfo();
                se_data.type = dropdown.value;
                assets = new AssetsInfo();
                assets.scenario = senario.text;

                se_data.assets = assets;
                CreateManager.Instance.dataList.Add(se_data);
                break;
            default:
                break;
        }
        
    }

    public void SaveScript()
    {
        string datas = UtilMethods<List<BaseData>>.EntityToJson(CreateManager.Instance.dataList);
        string path = Application.dataPath + "/test2.json";

        if (!File.Exists(path))
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            fs.Close();
        }

        File.WriteAllText(path, datas);
    }
}
