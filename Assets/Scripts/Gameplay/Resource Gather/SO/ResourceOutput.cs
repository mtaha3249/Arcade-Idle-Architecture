using System;
using UnityEngine;

/// <summary>
/// Base Currency class
/// </summary>
[Serializable]
public class Currency
{
    public ResourceUI _currencyUI;
    public GenericReference<int> _currencyCollected;
}

/// <summary>
/// Exchange currency
/// This class contain information of exchange any currency like give x resource and get y currency
/// </summary>
[Serializable]
public class CurrencyExchange : Currency
{
    [Tooltip("Currency Exchange rate i.e. 1 GreenWood = 0.25 Coins")]
    public float _currencyAmount;
}

[CreateAssetMenu(fileName = "Variable-Resource-Output", menuName = "Arcade Idle/Resource/Resource Output",
    order = 1)]
public class ResourceOutput : AutoCollectionResource
{
    public CurrencyExchange _currency;

    /// <summary>
    /// Give Currency Value
    /// </summary>
    public int CurrencyValue
    {
        get => _currency._currencyCollected.Value;
        set => _currency._currencyCollected.Value = value;
    }
}