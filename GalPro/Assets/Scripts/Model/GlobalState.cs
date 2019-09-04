using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalState
{
    public static Dictionary<string, UnityEngine.Object> assetPool = new Dictionary<string, UnityEngine.Object>();

    public static UnityEngine.Object GetAsset(Asset asset, Type type)
    {
        if (assetPool.ContainsKey(asset.name))
        {
            return assetPool[asset.name];
        }
        else
        {
            UnityEngine.Object obj = LoadAssets.Instance.LoadAndGetBundleAssets(asset.path, type)[0];
            assetPool.Add(asset.name, obj);
            return obj;
        }
    }

    public static void PoolClear()
    {
        assetPool.Clear();
    }
}

