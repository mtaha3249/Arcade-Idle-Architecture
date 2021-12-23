using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Manager for upgrade any item
/// </summary>
public class UpgradeUIManager : BaseUIManager<Item, Button>
{
    [SerializeField] private Transform _parent;
    private List<BaseItemUI<Item, Button>> _itemsInstantiated = new List<BaseItemUI<Item, Button>>();

    /// <summary>
    /// When item Initialized
    /// </summary>
    /// <param name="_itemObject">item to initialized</param>
    /// <param name="OnUpgrade">Event Raised when click on upgrade button</param>
    protected override void OnInit(Item _itemObject, Action<Item, Button> OnUpgrade)
    {
        _itemObject.Init();
        GameObject _itemInstantiated = Instantiate(_itemObject.ItemUI._prefab, _parent);
        BaseItemUI<Item, Button> _item = _itemInstantiated.GetComponent<BaseItemUI<Item, Button>>();
        _itemsInstantiated.Add(_item);
        _item.Init(_itemObject, OnUpgrade);
    }

    /// <summary>
    /// Refresh UI
    /// check if item can be purchased more
    /// </summary>
    public override void OnItemClicked()
    {
        foreach (var itemUI in _itemsInstantiated)
        {
            Upgradeable _upgradeable = (Upgradeable) itemUI.Item.IPurchase;
            itemUI.Button.interactable = _upgradeable.CanPurchase();
        }
    }

    /// <summary>
    /// Calls when item gets visible
    /// like reopen store
    /// </summary>
    public override void OnVisible()
    {
        base.OnVisible();

        foreach (var itemUI in _itemsInstantiated)
        {
            itemUI.OnVisible();
        }
    }
}