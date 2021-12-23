using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public static class DBManager
{
    private static Dictionary<string, float[]> _dictionary = new Dictionary<string, float[]>();

    /// <summary>
    /// Add New Entry in the local Database
    /// </summary>
    /// <param name="Key">Key of the new entry</param>
    /// <param name="Value">Value of the new entry</param>
    public static void AddEntry(string Key, float[] Value)
    {
        _dictionary.Add(Key, Value);
    }

    /// <summary>
    /// Get Entry by giving a key from Local Database
    /// </summary>
    /// <param name="Key">Key to fetch</param>
    /// <returns>Value of the Key</returns>
    public static float[] GetEntry(string Key)
    {
        try
        {
            return _dictionary[Key];
        }
        catch (Exception e)
        {
            Debug.LogError($"The Key : {Key} you want to access is Invalid. This Key doesn't exist in the Database");
            return null;
        }
    }

    /// <summary>
    /// Get Item Value of the given key by providing item level
    /// </summary>
    /// <param name="Key">Key to fetch</param>
    /// <param name="ItemLevel">Item Level to fetch</param>
    /// <returns>Value on that item level</returns>
    public static float GetItemValue(string Key, int ItemLevel)
    {
        return GetEntry(Key)[ItemLevel];
    }
    
    /// <summary>
    /// Get Variable name declared by developer
    /// </summary>
    /// <param name="expr">Input the variable</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>name of that variable declared by Developer</returns>
    public static string GetVariableName<T>(Expression<Func<T>> expr)
    {
        var body = (MemberExpression)expr.Body;
        return body.Member.Name;
    }
}
