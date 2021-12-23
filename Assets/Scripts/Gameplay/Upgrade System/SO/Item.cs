using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Arcade Idle/Store/Item", order = 1)]
public class Item : BaseSO
{
    [SerializeField] private ItemUIHolderWithPrefab itemUI;

    [SerializeField, RequireInterface(typeof(IInit))]
    private Object _toPurchase;
    
    // lock information to be added
    public IInit IInit => (IInit) _toPurchase;
    public IPurchase IPurchase => (IPurchase) _toPurchase;
    
    /// <summary>
    /// Unique Key of the item
    /// </summary>
    private string _ItemKey
    {
        get => string.Format("{0}_{1}", itemUI._name, InternalID);
    }

    /// <summary>
    /// Item UI Element Data
    /// Sprite, Prefab
    /// </summary>
    public ItemUIHolderWithPrefab ItemUI
    {
        get => itemUI;
    }

    /// <summary>
    /// Initialize Item
    /// </summary>
    public void Init()
    {
        IInit.Init();
    }
}