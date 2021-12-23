using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI manager of hand-overing resource to player
/// </summary>
public class AutoCollectionUIManager : BaseUIManager<AutoCollectionResource, Button>
{
    [SerializeField] private Transform _parent;

    private List<BaseItemUI<AutoCollectionResource, Button>> _itemsInstantiated =
        new List<BaseItemUI<AutoCollectionResource, Button>>();

    /// <summary>
    /// Initialize item collected
    /// </summary>
    /// <param name="_itemObject">Resource to handover</param>
    /// <param name="OnUpgrade">Event called when user want to collect resource</param>
    protected override void OnInit(AutoCollectionResource _itemObject, Action<AutoCollectionResource, Button> OnUpgrade)
    {
        GameObject _itemInstantiated = Instantiate(_itemObject._prefab, _parent);
        BaseItemUI<AutoCollectionResource, Button> _item =
            _itemInstantiated.GetComponent<BaseItemUI<AutoCollectionResource, Button>>();
        _itemsInstantiated.Add(_item);
        _item.Init(_itemObject, OnUpgrade);
    }

    /// <summary>
    /// Called when items re-visible
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