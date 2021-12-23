using System;
using UnityEngine;

/// <summary>
/// Purchaseable Currency
/// This currency is the amount of price need to purchase item
/// </summary>
[Serializable]
public class PurchaseableCurrency : Currency
{
    [SerializeField] private string _amountToPurchaseID = "DamagePrice";
    [Tooltip("Amount Required to Purchase item"), SerializeField]
    private int _amountToPurchase;
    private int _currentAmount;

    /// <summary>
    /// Purchase id from meta/economy
    /// </summary>
    public string AmountToPurchaseID => _amountToPurchaseID;
    
    /// <summary>
    /// Amount needed to purchase
    /// </summary>
    public int AmountToPurchase
    {
        get
        {
            _currentAmount = _currentAmount == 0 ? _amountToPurchase : _currentAmount;
            return _currentAmount;
        }
        set => _currentAmount = value;
    }
}

[CreateAssetMenu(fileName = "Upgradeable", menuName = "Arcade Idle/Store/Upgradeable Item", order = 1)]
public class Upgradeable : BaseSO, IInit, IPurchase
{
    [SerializeField] private string _ID;
    [SerializeField] private int _itemLevel = 1;
    public PurchaseableCurrency _currency;

    private float _value;
    private float _currentValue = 0;
    private int _currentItemLevel = 0;

    /// <summary>
    /// Value of the item
    /// </summary>
    public float Value
    {
        get
        {
            _currentValue = _currentValue == 0 ? _value : _currentValue;
            return _currentValue;
        }
        set => _currentValue = value;
    }

    /// <summary>
    /// Item Key used for saving
    /// </summary>
    private string _ItemKey => string.Format($"{_ID}_{InternalID}");

    /// <summary>
    /// Item Level
    /// This variable indicate the Level of Item
    /// </summary>
    public int ItemLevel
    {
        get
        {
            _currentItemLevel = _currentItemLevel == 0 ? _itemLevel : _currentItemLevel;
            return _currentItemLevel;
        }
        set
        {
            _currentItemLevel = value;
            Value = DBManager.GetItemValue(_ID, value);
            _currency.AmountToPurchase = (int) DBManager.GetItemValue(_currency.AmountToPurchaseID, value);
            SaveLoad.Save(_ItemKey, _currentItemLevel);
        }
    }

    /// <summary>
    /// Initialize Upgrade for given key
    /// </summary>
    /// <param name="_itemKey">Key</param>
    public void Init()
    {
        ItemLevel =
            SaveLoad.Fetch(new SaveKeys.Keys<int>(_ItemKey, 1));

        // read value from economy
        Value = DBManager.GetItemValue(_ID, ItemLevel);
        _currency.AmountToPurchase = (int) DBManager.GetItemValue(_currency.AmountToPurchaseID, ItemLevel);
    }

    /// <summary>
    /// Purchase item and deduct money
    /// </summary>
    /// <param name="arg"></param>
    public void Purchase(params object[] arg)
    {
        if (CanPurchase())
        {
            _currency._currencyCollected.Value -= _currency.AmountToPurchase;
            GameEvent _event = (GameEvent) arg[2];
            GameEvent _currencyChange = (GameEvent) arg[3];
            _event.Raise(arg[0], arg[1]);
            _currencyChange.Raise();
        }
    }

    /// <summary>
    /// Can Purchase Item
    /// </summary>
    /// <returns>bool value</returns>
    public bool CanPurchase()
    {
        return _currency._currencyCollected.Value >= _currency.AmountToPurchase;
    }
}