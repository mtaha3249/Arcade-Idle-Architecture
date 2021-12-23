using System;
using UnityEngine;

/// <summary>
/// Base item UI
/// This class Handles Initialization, OnButton Click and when this item Visible
/// </summary>
/// <typeparam name="T">Item Type</typeparam>
/// <typeparam name="T2">UI Button</typeparam>
public class BaseItemUI<T, T2> : MonoBehaviour
{
    [SerializeField] protected T2 _button;

    private bool isInitialized = false;
    protected T _item;

    public T2 Button => _button;

    public T Item => _item;

    protected Action<T, T2> _OnUpgrade;

    /// <summary>
    /// UI Item Prefab
    /// </summary>
    /// <param name="_item">Item to show</param>
    /// <param name="OnUpgrade">On Upgrade done</param>
    public void Init(T _item, Action<T, T2> OnUpgrade)
    {
        if (isInitialized)
            return;
        
        _OnUpgrade = OnUpgrade;
        this._item = _item;
        OnInit(_item, _button);
        OnVisible();
        isInitialized = true;
    }

    /// <summary>
    /// Calls on initialization
    /// </summary>
    /// <param name="_item">Item to initialize</param>
    /// <param name="_button">Button to initialize</param>
    protected virtual void OnInit(T _item, T2 _button)
    {
    }

    /// <summary>
    /// Calls on button clicked
    /// Raise Event which is passed from Manager to UI Manager to this item
    /// </summary>
    protected virtual void OnClick()
    {
        _OnUpgrade?.Invoke(_item, _button);
    }

    /// <summary>
    /// Calls when item gets visible
    /// like reopen store
    /// </summary>
    public virtual void OnVisible()
    {
    }
}