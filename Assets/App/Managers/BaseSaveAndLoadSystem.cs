using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseSaveAndLoadSystem : MonoBehaviour
{
    public string Header = "yl_";
    public Dictionary<string, int>    IntegerPairs;
    public Dictionary<string, float>  FloatPairs;
    public Dictionary<string, bool>   BoolPairs;
    public Dictionary<string, string> StringPairs;

    private void Awake()
    {
        IntegerPairs = new Dictionary<string, int>();
        FloatPairs = new Dictionary<string, float>();
        BoolPairs = new Dictionary<string, bool>();
        StringPairs = new Dictionary<string, string>();

        
    }

    public void Save(string key, int value)
    {
        IntegerPairs.Add(key, value);
    }

    public void Save(string key, float value)
    {
        FloatPairs.Add(key, value);
    }

    public void Save(string key, bool value)
    {
        BoolPairs.Add(key, value);
    }

    public void Save(string key, string value)
    {
        StringPairs.Add(key, value);
    }

    public bool Load(string key, out int val)
    {
        return IntegerPairs.TryGetValue(key, out val);
    }

    public bool Load(string key, out float val)
    {
        return FloatPairs.TryGetValue(key, out val);
    }

    public bool Load(string key, out bool val)
    {
        return BoolPairs.TryGetValue(key, out val);
    }

    public bool Load(string key, out string val)
    {
        return StringPairs.TryGetValue(key, out val);
    }

    private bool HasAnyPair(string key)
    {
        if(HasIntPair(key) || HasFloatPair(key) || HasBoolPair(key) || HasStringPair(key))
        {
            return true;
        }
        return false;
    }
    private bool HasIntPair(string key)
    {
        return IntegerPairs.ContainsKey(key);
    }

    private bool HasFloatPair(string key)
    {
        return FloatPairs.ContainsKey(key);
    }

    private bool HasBoolPair(string key)
    {
        return BoolPairs.ContainsKey(key);
    }

    private bool HasStringPair(string key)
    {
        return StringPairs.ContainsKey(key);
    }

    private void Save()
    {
        foreach (var item in IntegerPairs)
        {
            PlayerPrefs.SetInt(item.Key, item.Value);
        }

        foreach (var item in FloatPairs)
        {
            PlayerPrefs.SetFloat(item.Key, item.Value);
        }

        foreach (var item in BoolPairs)
        {
            PlayerPrefs.SetInt(item.Key, item.Value ? 1 : 0);
        }

        foreach (var item in StringPairs)
        {
            PlayerPrefs.SetString(item.Key, item.Value);
        }
        PlayerPrefs.Save();
    }


    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }
}
