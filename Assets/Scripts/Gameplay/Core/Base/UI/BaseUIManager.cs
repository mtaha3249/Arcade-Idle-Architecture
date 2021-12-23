using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base UI Manager
/// This class Handles Initialization, OnButton Click and when this item Visible
/// </summary>
/// <typeparam name="T">Item Type</typeparam>
/// <typeparam name="T2">UI Button</typeparam>
public abstract class BaseUIManager<T, T2> : MonoBehaviour
{
    private bool isInitialized = false;

    /// <summary>
    /// Initialized UI Manager
    /// </summary>
    /// <param name="_item">Items to Initialized</param>
    /// <param name="OnUpgrade">Callback on Item Upgraded</param>
    public void Init(List<T> _item, Action<T,T2> OnUpgrade)
    {
        if (isInitialized)
            return;

        foreach (var _itemObject in _item)
        {
            OnInit(_itemObject, OnUpgrade);
        }

        isInitialized = true;
    }
    
    /// <summary>
    /// Calls on the item click
    /// for example click on upgrade button in prefab
    /// </summary>
    public virtual void OnItemClicked()
    {
        
    }

    /// <summary>
    /// Calls when item gets visible
    /// like reopen store
    /// </summary>
    public virtual void OnVisible()
    {
        
    }

    /// <summary>
    /// Calls on initialization of item passed in _itemObject
    /// </summary>
    /// <param name="_itemObject">Item to initialize</param>
    /// <param name="OnUpgrade">Event Raised when item upgraded/purchased and pass back to Manager</param>
    protected abstract void OnInit(T _itemObject, Action<T, T2> OnUpgrade);
}
