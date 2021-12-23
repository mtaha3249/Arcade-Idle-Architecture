using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Basic UI Handler
/// This one specifically used for Upgrade Manager
/// </summary>
public class ItemUI : BaseItemUI<Item, Button>
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _icon;

    /// <summary>
    /// Calls on init
    /// </summary>
    /// <param name="_item">item to initialize</param>
    /// <param name="_button">button</param>
    protected override void OnInit(Item _item, Button _button)
    {
        base.OnInit(_item, _button);
        _button.onClick.AddListener(() => { OnClick(); });
        _name.text = _item.ItemUI._name;
        _icon.sprite = _item.ItemUI._icon;
    }

    /// <summary>
    /// Calls when item gets visible
    /// like reopen store
    /// </summary>
    public override void OnVisible()
    {
        base.OnVisible();
        Upgradeable _upgradeable = (Upgradeable) _item.IPurchase;
        _button.interactable = _upgradeable.CanPurchase();
    }
}