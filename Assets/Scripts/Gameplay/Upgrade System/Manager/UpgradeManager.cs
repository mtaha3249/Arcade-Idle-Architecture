using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Upgrade Manager of player
/// </summary>
[RequireComponent(typeof(UpgradeUIManager))]
public class UpgradeManager : BasePurchaseManager<Item, Button>
{
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Calls when item is upgraded
    /// </summary>
    /// <param name="obj">Item to upgrade</param>
    /// <param name="obj2">Button clicked to upgrade</param>
    public override void OnUpgrade(Item obj, Button obj2)
    {
        // purchase item and deduct money
        obj.IPurchase.Purchase(obj, obj2, UpgradeEventRaise, CurrencyEventRaise);
        // refresh UI
        _UIManager.OnItemClicked();
    }
}