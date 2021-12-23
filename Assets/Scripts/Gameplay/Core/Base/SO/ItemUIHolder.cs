using UnityEngine;

/// <summary>
/// UI Holder containing name and icon
/// </summary>
public class ItemUIHolder : BaseSO
{
    public string _name;
    public Sprite _icon;
}

/// <summary>
/// UI Holder containing name, icon and prefab
/// </summary>
public class ItemUIHolderWithPrefab : BaseSO
{
    public string _name;
    public Sprite _icon;
    public GameObject _prefab;
}
