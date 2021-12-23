using UnityEngine;

[CreateAssetMenu(fileName = "Variable-Auto-Resource-Output", menuName = "Arcade Idle/Resource/Auto Resource Output",
    order = 1)]
public class AutoCollectionResource : BaseSO
{
    [Multiline, SerializeField] private string developerInstuction;
    public ResourceUI _UI;
    public GenericReference<int> _currentCollected;

    [Header("UI Data")] public GameObject _prefab;

    /// <summary>
    /// Item Key used for saving
    /// </summary>
    private string _ItemKey => string.Format("{0}_{1}", _UI._name, _UI.InternalID);

    /// <summary>
    /// Give Current Value
    /// </summary>
    public int CurrentValue
    {
        get
        {
            if (_currentCollected.Value == 0)
            {
                _currentCollected.Value = SaveLoad.Fetch<int>(new SaveKeys.Keys<int>(_ItemKey, 0));
            }

            return _currentCollected.Value;
        }
        set
        {
            _currentCollected.Value = value;
            SaveLoad.Save(_ItemKey, value);
        }
    }
}