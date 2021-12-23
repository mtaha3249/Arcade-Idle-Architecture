using UnityEngine;
using UnityEngine.UI;

public class MarketItemUI : BaseItemUI<ResourceOutput, Button>
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _givenIcon;
    [SerializeField] private Text _givenTitle;
    [SerializeField] private ResourceOutput item;
    private int currencyAmount;

    protected override void OnInit(ResourceOutput _item, Button _button)
    {
        base.OnInit(_item, _button);

        item = _item;
        _button.onClick.AddListener(() => { OnClick(); });

        _name.text = item._UI._name;
        _icon.sprite = item._UI._icon;

        _givenIcon.sprite = item._currency._currencyUI._icon;
        currencyAmount = _item.CurrencyValue;
    }

    protected override void OnClick()
    {
        base.OnClick();
        
        string _title = item._currency._currencyUI._name;
        int amountToGive = (int) (item.CurrentValue * item._currency._currencyAmount);
        _givenTitle.text = string.Format("{0}{1}", amountToGive, _title);
    }

    public override void OnVisible()
    {
        base.OnVisible();

        _button.interactable = true;
        string _title = item._currency._currencyUI._name;
        int amountToGive = (int) (item.CurrentValue * item._currency._currencyAmount);
        _givenTitle.text = string.Format("{0}{1}", amountToGive, _title);
    }
}