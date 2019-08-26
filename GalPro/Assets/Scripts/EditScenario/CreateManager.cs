using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    [SerializeField]
    GameObject CreatePanel;
    [SerializeField]
    GameObject Buttons;

    private string currentName;

    public List<BaseData> dataList;
    public static CreateManager Instance;

    private void Awake()
    {
        if (dataList == null)
        {
            dataList = new List<BaseData>();
        }
        Instance = this;
    }

    public void CreateNewScenario()
    {
        CreatePanel.SetActive(true);
        Buttons.SetActive(false);
    }

    public void BackMain()
    {
        CreatePanel.SetActive(false);
        Buttons.SetActive(true);
    }
}
