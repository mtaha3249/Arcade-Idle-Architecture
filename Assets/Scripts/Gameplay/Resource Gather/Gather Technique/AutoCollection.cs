using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class gather resource while game is playing, but when quit stop gathering
/// </summary>
public class AutoCollection : MonoBehaviour, IBaseGatherResource
{
    [SerializeField]
    // private GenericReference<Upgradeable> _totalResource;
    private Upgradeable _totalResource;

    [SerializeField] private Text _collected;

    public Upgradeable TotalResource => _totalResource;
    // public GenericReference<Upgradeable> TotalResource => _totalResource;

    [SerializeField] private GenericReference<float> _totalTime;
    private float elapsedTime = 0;

    private int _gatheredResource;

    private void Update()
    {
        GatherResource();
    }

    /// <summary>
    /// Return Gathered Resource
    /// </summary>
    /// <returns>Amount of resource gathered</returns>
    public int GiveResource()
    {
        ResetGather();
        int gathered = _gatheredResource;
        _gatheredResource = 0;
        return gathered;
    }

    /// <summary>
    /// Reset Gather
    /// </summary>
    void ResetGather()
    {
        elapsedTime = 0;
    }

    /// <summary>
    /// Gather resource
    /// </summary>
    public void GatherResource()
    {
        if (_gatheredResource >= _totalResource.Value)
            return;
        // if (_gatheredResource >= _totalResource.Value.Value)
        //     return;

        elapsedTime += Time.deltaTime;
        _gatheredResource = (int) ((elapsedTime / _totalTime.Value) * _totalResource.Value);
        _collected.text = string.Format("{0}/{1}", _gatheredResource, _totalResource.Value);
        // _gatheredResource = (int) ((elapsedTime / _totalTime.Value) * _totalResource.Value.Value);
        // _collected.text = string.Format("{0}/{1}", _gatheredResource, _totalResource.Value.Value);
    }
}