using UnityEngine;

[RequireComponent(typeof(AutoCollection))]
public class CollectResourceAuto : MonoBehaviour, IGenericCallback
{
    private AutoCollection _autoCollection;
    public GameEvent _onResourceCollected;
    [SerializeField] private AutoCollectionResource _autoCollectionResource;
    
    public AutoCollectionResource AutoCollectionResource
    {
        get => _autoCollectionResource;
        set => _autoCollectionResource = value;
    }

    private void Start()
    {
        _autoCollection = GetComponent<AutoCollection>();
    }

    /// <summary>
    /// Get Game object to collect from
    /// </summary>
    /// <param name="param"></param>
    public void OnEventRaisedCallback(params object[] param)
    {
        GameObject ObjectToCollectFrom = (GameObject) param[0];

        if (ObjectToCollectFrom != null)
        {
            if (ObjectToCollectFrom == gameObject)
            {
                _autoCollectionResource.CurrentValue =
                    _autoCollectionResource.CurrentValue + _autoCollection.GiveResource();
                _onResourceCollected.Raise();
            }
        }
    }
}
