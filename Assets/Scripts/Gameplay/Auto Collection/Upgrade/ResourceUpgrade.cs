using UnityEngine;

[RequireComponent(typeof(CollectResourceAuto))]
public class ResourceUpgrade : MonoBehaviour, IGenericCallback
{
    private CollectResourceAuto _autoCollection;
    [SerializeField] private Item _item;
    [SerializeField] private AutoCollectionResourceUpgrade _autoCollectionResourceUpgrade;

    private void Start()
    {
        _autoCollection = GetComponent<CollectResourceAuto>();
        _autoCollectionResourceUpgrade.Init();
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        Item _itemPurchased = (Item) param[0];
        if (_itemPurchased == _item)
        {
            _autoCollection.AutoCollectionResource = _autoCollectionResourceUpgrade.UpdateAutoCollectionResource();
        }
    }
}