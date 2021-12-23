using UnityEngine;

[CreateAssetMenu(fileName = "Variable-Auto-Resource-Output", menuName = "Arcade Idle/Resource/Auto Resource Switching",
    order = 1)]
public class AutoCollectionResourceUpgrade : BaseSO
{
    [Multiline, SerializeField] private string developerInstuction;
    [SerializeField] private int _itemLevel = 1;
    [SerializeField] private AutoCollectionResource[] _autoCollection;

    private int _currentItemLevel = 0;

    /// <summary>
    /// Item Key used for saving
    /// </summary>
    private string _ItemKey => string.Format("{0}_{1}", name, InternalID);

    /// <summary>
    /// Item Level
    /// This variable indicate the Level of Item
    /// </summary>
    public int ItemLevel
    {
        get
        {
            _currentItemLevel = _currentItemLevel == 0 ? _itemLevel : _currentItemLevel;
            return _currentItemLevel;
        }
        set
        {
            _currentItemLevel = value;
            SaveLoad.Save(_ItemKey, _currentItemLevel);
        }
    }

    /// <summary>
    /// Initialize Upgrade for given key
    /// </summary>
    /// <param name="_itemKey">Key</param>
    public void Init()
    {
        ItemLevel =
            SaveLoad.Fetch(new SaveKeys.Keys<int>(_ItemKey, 1));
    }

    /// <summary>
    /// Update Auto Collection Resource
    /// Add Item Level and give resource SO
    /// </summary>
    /// <returns>Updated Resource SO</returns>
    public AutoCollectionResource UpdateAutoCollectionResource()
    {
        ItemLevel = ItemLevel + 1;
        return _autoCollection[ItemLevel - 1];
    }
}