using UnityEngine;

public class ManualCollection : MonoBehaviour, IBaseGatherResource
{
    [SerializeField]
    private GenericReference<int> _totalResource;
    
    /// <summary>
    /// Output Resource Added
    /// </summary>
    [SerializeField]
    private ResourceOutput _outputResource;
    
    /// <summary>
    /// Event Raised On Resource Gathered
    /// </summary>
    [SerializeField]
    private GameEvent _onResourceCollected;
    
    /// <summary>
    /// Percent of Damage I am getting
    /// </summary>
    private float percentDamage;

    /// <summary>
    /// Resources Left I can give
    /// </summary>
    private float _resourceLeft;
    /// <summary>
    /// Amount to give per hit
    /// </summary>
    private float _amountToGive;

    private void Awake()
    {
        _resourceLeft = _totalResource.Value;
    }

    /// <summary>
    /// Called when I get damage
    /// </summary>
    /// <param name="_damageAmount">Contains information of current damage and total health</param>
    public void OnDamageRecieved(Damage _damageAmount)
    {
        // find percentage of damage I am recieving
        percentDamage = (_damageAmount._damageAmount / _damageAmount._maxHealth) * 100;
        // calculate amount to resource to give on one Hit
        _amountToGive = (percentDamage / 100) * _totalResource.Value;
        GatherResource();
    }

    /// <summary>
    /// Gather resource and give to player
    /// </summary>
    public void GatherResource()
    {
        // checking if my resources goes below zero after subtraction, then this is wrong.
        // if we didn't check this means we probably give more resource than we ever have.
        if (_resourceLeft - _amountToGive > 0)
        {
            // give resource of _amountToGive
            // remove resource from those left behind
            _resourceLeft -= _amountToGive;
            _outputResource.CurrentValue = _outputResource.CurrentValue + (int)_amountToGive;
        }
        else
        {
            // give resources left
            _outputResource.CurrentValue = _outputResource.CurrentValue + (int)_resourceLeft;
        }

        _onResourceCollected.Raise();
    }
}