using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUIManager : BaseUIManager<ResourceOutput, Button>
{
    [SerializeField] private Transform _parent;

    private List<BaseItemUI<ResourceOutput, Button>> _itemsInstantiated = new List<BaseItemUI<ResourceOutput, Button>>();

    protected override void OnInit(ResourceOutput _itemObject, Action<ResourceOutput, Button> OnUpgrade)
    {
        GameObject _itemInstantiated = Instantiate(_itemObject._prefab, _parent);
        BaseItemUI<ResourceOutput, Button> _item = _itemInstantiated.GetComponent<BaseItemUI<ResourceOutput, Button>>();
        _itemsInstantiated.Add(_item);
        _item.Init(_itemObject, OnUpgrade);
    }

    public override void OnVisible()
    {
        base.OnVisible();
        
        foreach (var itemUI in _itemsInstantiated)
        {
            itemUI.OnVisible();
        }
    }
}
