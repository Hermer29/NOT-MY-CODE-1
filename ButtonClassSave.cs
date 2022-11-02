using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButtonClassSave
{
    public static void SaveToPlayerPrefs<T>(T data, string key)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
        var a = JsonUtility.ToJson(data);
        Debug.Log(a);
    }
    public static T LoadFromPlayerPrefs<T>(T data, string key )
    {
        if (JsonUtility.FromJson<T>(PlayerPrefs.GetString(key)) == null)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
            var a = JsonUtility.ToJson(data);
            Debug.Log(a);
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        }
        else
        {
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        }
    }
    public static void ResetFromPlayerPrefs(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}