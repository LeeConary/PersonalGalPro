using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class UtilMethods : MonoBehaviour
{
    public static GameObject GetOb(string father, string aim)
    {
        Transform[] transforms = GameObject.Find(father).GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].gameObject.name.Equals(aim))
            {
                return transforms[i].gameObject;
            }
        }
        return null;
    }

    public static GameObject GetOb(GameObject father, string aim)
    {
        Transform[] transforms = father.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].gameObject.name.Equals(aim))
            {
                return transforms[i].gameObject;
            }
        }
        return null;
    }
    public static GameObject GetUIOb(string father, string aim)
    {
        RectTransform[] transforms = GameObject.Find(father).GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].gameObject.name.Equals(aim))
            {
                return transforms[i].gameObject;
            }
        }
        return null;
    }

    public static GameObject GetUIOb(GameObject father, string aim)
    {
        RectTransform[] transforms = father.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].gameObject.name.Equals(aim))
            {
                return transforms[i].gameObject;
            }
        }
        return null;
    }
}

public class UtilMethods<T>: MonoBehaviour
{
    /// <summary>
    /// 获取组件
    /// </summary>
    /// <param name="name">需要获取组件的GameObject对象</param>
    /// <returns></returns>
    public new static T GetComponent(string name)
    {
        return GameObject.Find(name).GetComponent<T>();
    }

    public new static T GetComponent(string father, string aim)
    {
        return UtilMethods.GetOb(father, aim).GetComponent<T>();
    }
    public new static T GetComponent(GameObject father, string aim)
    {
        return UtilMethods.GetOb(father, aim).GetComponent<T>();
    }
    /// <summary>
    /// 获取全部标签为tag的GameObject对象的某一组件
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static T[] GetComponents(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        T[] t = new T[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            t[i] = objs[i].GetComponent<T>();
        }
        return t;
    }

    /// <summary>
    /// 获取一个对象的子对象的组件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T[] GetComponentsInChildren(string name)
    {
        T[] t = GameObject.Find(name).GetComponentsInChildren<T>();
        return t;
    }

    /// <summary>
    /// 获取一个对象的子对象的组件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T[] GetComponentsInChildren(string parent, string aim)
    {
        T[] t = UtilMethods.GetOb(parent, aim).GetComponentsInChildren<T>();
        return t;
    }

    /// <summary>
    /// 解析json文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<T> JsonToEntity(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        string json = File.ReadAllText(path);
        List<T> list = JsonConvert.DeserializeObject<List<T>>(json);
        return list;
    }

    public static List<T> JsonToEntity(string content, bool isContent)
    {
        if (isContent)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(content);
            return list;
        }
        else
        {
            return JsonToEntity(content);
        }
    }

    /// <summary>
    /// 序列化为json文件
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static string EntityToJson(T entity)
    {
        string json = JsonConvert.SerializeObject(entity);
        return json;
    }
}