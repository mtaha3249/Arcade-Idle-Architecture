using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Auto Resource gather Manager
/// </summary>
[RequireComponent(typeof(UpgradeUIManager))]
[RequireComponent(typeof(AutoCollectionUIManager))]
public class AutoCollectionManager : BasePurchaseManager<Item, Button>, IGenericCallback
{
    [Header("Auto Collection")]
    [SerializeField] private GameEvent _collectEventRaise;
    [SerializeField] private List<AutoCollectionResource> _collectItems;
    
    private AutoCollectionUIManager _autoCollectUIManager;
    private GameObject _raisedGameObject;

    private void Awake()
    {
        _UIManager = GetComponent<UpgradeUIManager>();
        _autoCollectUIManager = GetComponent<AutoCollectionUIManager>();
        Init();
    }

    /// <summary>
    /// Upgrade Item
    /// This increase the amount to item collected or reduce timer etc
    /// </summary>
    /// <param name="obj">item to upgrade</param>
    /// <param name="obj2"></param>
    public override void OnUpgrade(Item obj, Button obj2)
    {
        // purchase item and deduct money
        obj.IPurchase.Purchase(obj, obj2, UpgradeEventRaise, CurrencyEventRaise);
        // refresh UI
        _UIManager.OnItemClicked();
    }

    /// <summary>
    /// Initialize UI Manager which handover resource to player
    /// </summary>
    public override void Init()
    {
        base.Init();
        _autoCollectUIManager.Init(_collectItems, OnUpgrade);
    }

    /// <summary>
    /// Hand-over resource to player when user clicked on button in UI
    /// </summary>
    /// <param name="obj">resource collected till now</param>
    private void OnUpgrade(AutoCollectionResource obj, Button obj2)
    {
        _collectEventRaise.Raise(_raisedGameObject);
        _autoCollectUIManager.OnItemClicked();
    }

    /// <summary>
    /// Event Callback when user entered to auto collection zone
    /// </summary>
    /// <param name="param"></param>
    public void OnEventRaisedCallback(params object[] param)
    {
        if (param.Length > 1)
            _raisedGameObject = (GameObject) param[1];
    }
}