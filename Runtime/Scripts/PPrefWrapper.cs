using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PPrefWrapper
{
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public static string GetString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}