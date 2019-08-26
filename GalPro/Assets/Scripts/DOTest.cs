using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;

[Serializable]
class Model
{
    int type;
    string asset;
    TextAsset text;
    AudioClip audio;
    Texture2D texture;

    public Model(int type, string asset, TextAsset text, AudioClip audio, Texture2D texture)
    {
        this.type = type;
        this.asset = asset;
        this.text = text;
        this.audio = audio;
        this.texture = texture;
    }

    public Model() { }
}

public class DOTest : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
