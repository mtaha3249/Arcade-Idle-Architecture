using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Purchase Manager
/// Handle Initialization of Manager and Upgrading Item
/// </summary>
/// <typeparam name="T">Item Type</typeparam>
/// <typeparam name="T2">UI Button</typeparam>
public abstract class BasePurchaseManager<T, T2> : MonoBehaviour
{
    protected BaseUIManager<T, T2> _UIManager;
    [SerializeField] private GameEvent _upgradeEventRaise;
    [SerializeField] private GameEvent _currencyEventRaise;

    public GameEvent CurrencyEventRaise => _currencyEventRaise;

    public GameEvent UpgradeEventRaise => _upgradeEventRaise;

    [SerializeField] private List<T> _items;

    private bool isInitialized = false;

    /// <summary>
    /// Initialize Upgrade Manager
    /// </summary>
    public virtual void Init()
    {
        if (isInitialized)
            return;

        _UIManager = GetComponent<BaseUIManager<T, T2>>();
        _UIManager.Init(_items, OnUpgrade);
        isInitialized = true;
    }

    /// <summary>
    /// On Item Upgrade Event
    /// </summary>
    /// <param name="obj">object to upgrade</param>
    public virtual void OnUpgrade(T obj, T2 obj2)
    {
        _upgradeEventRaise.Raise(obj, obj2);
        _UIManager.OnItemClicked();
    }
}