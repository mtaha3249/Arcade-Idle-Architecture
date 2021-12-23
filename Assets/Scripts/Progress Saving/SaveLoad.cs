using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class SaveLoad
{
    /// <summary>
    /// Dictionary class containing key values
    /// </summary>
    [Serializable]
    public class DictionaryParsing
    {
        public string _key;
        public string _value;

        public DictionaryParsing(string key, string value)
        {
            _key = key;
            _value = value;
        }
    }

    /// <summary>
    /// Saving Data
    /// </summary>
    [Serializable]
    private class SavedInformation
    {
        public string hashValue;
        public List<DictionaryParsing> _savedData = new List<DictionaryParsing>();
    }

    private static Dictionary<string, string> _dictionary = new Dictionary<string, string>();
    private static SavedInformation _savedInformation = new SavedInformation();
    private static string fileName = "SaveData";
    private static bool isProgressLoaded = false;

    /// <summary>
    /// Save Progress by giving ID and value
    /// Don't Save Dictionary
    /// Array, List can be saved
    /// </summary>
    /// <param name="key">unique ID</param>
    /// <param name="value">value of the key</param>
    /// <typeparam name="T">type of data</typeparam>
    public static void Save<T>(string key, T value)
    {
        if (!isProgressLoaded)
            LoadProgress();

        if (_dictionary.ContainsKey(key))
            _dictionary[key] = JsonHelper.ToJson(new T[] {value});
        else
            _dictionary.Add(key, JsonHelper.ToJson(new T[] {value}));
    }

    /// <summary>
    /// Get Data from progress
    /// </summary>
    /// <param name="key">unique ID</param>
    /// <typeparam name="T">data type to get</typeparam>
    /// <returns>data parsed</returns>
    public static T Fetch<T>(SaveKeys.Keys<T> key)
    {
        if (!isProgressLoaded)
            LoadProgress();

        if (_dictionary.ContainsKey(key.Key))
            return JsonHelper.FromJson<T>(_dictionary[key.Key])[0];
        return key.defaultValue;
    }

    /// <summary>
    /// Save Progress
    /// Warning Costly process
    /// </summary>
    public static void SaveProgress()
    {
        string saveDataHashed = SavedJSON();
        File.WriteAllText(GetPath(), saveDataHashed);
    }

    /// <summary>
    /// Give Saved progress by parsing it to json
    /// </summary>
    /// <returns>json</returns>
    static string SavedJSON()
    {
        DictionaryToList();
        _savedInformation.hashValue =
            HashGenerator(JsonUtility.ToJson(_savedInformation._savedData, true));
        return JsonUtility.ToJson(_savedInformation, true);
    }

    /// <summary>
    /// Load Progress
    /// </summary>
    static void LoadProgress()
    {
        if (File.Exists(GetPath()))
        {
            string fileContent = File.ReadAllText(GetPath());
            _savedInformation = JsonUtility.FromJson<SavedInformation>(fileContent);
#if !UNITY_EDITOR
            //File tampering checks
            if (HashGenerator(JsonUtility.ToJson(_savedInformation._savedData, true)) !=
                 _savedInformation.hashValue)
            {
                DeleteProgress();
                SaveProgress();
                Debug.Log("File tampering detected. Resetting Progress");
            }
#endif
            Debug.Log("Game Load Successful: " + GetPath());
        }
        else
        {
            SaveProgress();
            Debug.Log("New Game Creation Successful: " + GetPath());
        }

        ListToDictionary();
        isProgressLoaded = true;
    }

    /// <summary>
    /// Generate Hash
    /// </summary>
    /// <param name="saveContent">content of hash</param>
    /// <returns>hash</returns>
    public static string HashGenerator(string saveContent)
    {
        SHA256Managed crypt = new SHA256Managed();
        string hash = string.Empty;
        byte[] crypto =
            crypt.ComputeHash(Encoding.UTF8.GetBytes(saveContent), 0, Encoding.UTF8.GetByteCount(saveContent));
        foreach (byte bit in crypto)
        {
            hash += bit.ToString("x2");
        }

        return hash;
    }

    /// <summary>
    /// get persistant path to the project
    /// </summary>
    /// <returns>persistant path</returns>
    static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, string.Concat(fileName, ".json"));
    }

    /// <summary>
    /// Delete progress
    /// </summary>
    public static void DeleteProgress()
    {
        if (File.Exists(GetPath()))
        {
            File.Delete(GetPath());
        }
    }

    /// <summary>
    /// Convert Dictionary to list so it can be saved
    /// </summary>
    static void DictionaryToList()
    {
        _savedInformation._savedData = new List<DictionaryParsing>();
        for (int i = 0; i < _dictionary.Count; i++)
            _savedInformation._savedData.Add(new DictionaryParsing(_dictionary.Keys.ElementAt(i),
                _dictionary.Values.ElementAt(i)));
    }


    /// <summary>
    /// Convert List to dictionary so can be readable
    /// </summary>
    static void ListToDictionary()
    {
        _dictionary = new Dictionary<string, string>();
        for (int i = 0; i < _savedInformation._savedData.Count; i++)
            _dictionary.Add(_savedInformation._savedData[i]._key, _savedInformation._savedData[i]._value);
    }

#if UNITY_EDITOR
    [MenuItem("Mood Games/Saved Data/Reset All Progress %#r")]
    static void ResetProgress()
    {
        DeleteProgress();
        EditorUtility.DisplayDialog("Reset Progress", "Saved Progress is Reset", "Ok");
    }

    [MenuItem("Mood Games/Saved Data/Open Save File %#o")]
    static void OpenSave()
    {
        Process.Start(Application.persistentDataPath);
    }
#endif
}

/// <summary>
/// Helper class to store any type of data
/// </summary>
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Value;
    }

    public static string ToJson<T>(T[] array, bool prettyPrint = false)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Value = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Value;
    }
}