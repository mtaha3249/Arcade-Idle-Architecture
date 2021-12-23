using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Damage
{
    public float _maxHealth;
    public float _damageAmount;
}

/// <summary>
/// Health of item
/// </summary>
public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private GenericReference<float> _maxHealth;
    private float currentHealth;

    [SerializeField] private UnityEvent<Damage> OnDamage;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private UnityEvent OnRespawn;

    private Damage _damage = new Damage();

    private void Awake()
    {
        _damage._maxHealth = _maxHealth.Value;
        Init();
    }

    /// <summary>
    /// Initialize Item Health
    /// </summary>
    public virtual void Init()
    {
        currentHealth = _maxHealth.Value;
    }

    /// <summary>
    /// Get Damage and deduct from health
    /// Check for dead too
    /// </summary>
    /// <param name="_damageAmount">amount of damage give to item</param>
    public virtual void GetDamage(float _damageAmount)
    {
        currentHealth -= _damageAmount;
        _damage._damageAmount = _damageAmount;
        OnDamage.Invoke(_damage);
        
        if (currentHealth <= 0)
            Dead();
    }

    /// <summary>
    /// Check for dead
    /// if condition match declare dead
    /// </summary>
    public virtual void Dead()
    {
        OnDeath.Invoke();
    }

    /// <summary>
    /// Respawn item by giving max health
    /// </summary>
    public virtual void Respawn()
    {
        currentHealth = _maxHealth.Value;
        OnRespawn.Invoke();
    }
}