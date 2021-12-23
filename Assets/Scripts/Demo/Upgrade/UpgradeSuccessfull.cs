using UnityEngine;
using UnityEngine.UI;

public class UpgradeSuccessfull : MonoBehaviour, IGenericCallback
{
    [SerializeField] private Item _item;

    public void OnEventRaisedCallback(params object[] param)
    {
        // core logic of the game on purchase
        // check if the amount collected left is will provide whole number then disable button etc
        Item _itemPurchased = (Item) param[0];
        Button _itembtn = (Button) param[1];
        Upgradeable _upgradeable = (Upgradeable) _itemPurchased.IPurchase;
        if (_itemPurchased == _item)
        {
            _upgradeable.ItemLevel = _upgradeable.ItemLevel + 1;
            // _itembtn.interactable = _upgradeable.CanPurchase();
            Debug.Log(_upgradeable.name + " is recieved to be upgraded");
        }
    }
}