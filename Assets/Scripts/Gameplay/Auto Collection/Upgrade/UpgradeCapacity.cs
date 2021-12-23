using UnityEngine;

[RequireComponent(typeof(AutoCollection))]
public class UpgradeCapacity : MonoBehaviour, IGenericCallback
{
    private AutoCollection _autoCollection;
    [SerializeField] private Item _item;

    private void Start()
    {
        _autoCollection = GetComponent<AutoCollection>();
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        Item _itemPurchased = (Item) param[0];
        if (_itemPurchased == _item)
        {
            _autoCollection.TotalResource.Value += 100;
            _autoCollection.TotalResource.ItemLevel += 1;
            // _autoCollection.TotalResource.Value.Value += 100;
            // _autoCollection.TotalResource.Value.ItemLevel += 1;
        }
    }
}