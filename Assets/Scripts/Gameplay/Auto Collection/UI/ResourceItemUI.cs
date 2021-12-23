using UnityEngine;
using UnityEngine.UI;

public class ResourceItemUI : BaseItemUI<AutoCollectionResource, Button>
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _icon;

    /// <summary>
    /// Update UI when initialized
    /// </summary>
    /// <param name="_item">Item to read</param>
    /// <param name="_button">Upgrade or collect button</param>
    protected override void OnInit(AutoCollectionResource _item, Button _button)
    {
        base.OnInit(_item, _button);
        _button.onClick.AddListener(() => { OnClick(); });
        _name.text = _item._UI._name;
        _icon.sprite = _item._UI._icon;
    }
}