using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class DOTest : MonoBehaviour
{
    void Start()
    {
        UnityEngine.Object[] ob = LoadAssets.Instance.LoadAndGetBundleAssets("characters/rita.asset", typeof(Sprite));
        SpriteRenderer sr = new GameObject().AddComponent<SpriteRenderer>();
        sr.sprite = ob[0] as Sprite;
    }
}
