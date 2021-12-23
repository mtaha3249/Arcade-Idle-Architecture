/// <summary>
/// Interface for health
/// </summary>
public interface IHealth
{
    /// <summary>
    /// Initialize Health
    /// </summary>
    public void Init();
    
    /// <summary>
    /// Recieve Damage of given Amount
    /// </summary>
    /// <param name="_damageAmount">amount of damage given</param>
    public void GetDamage(float _damageAmount);

    /// <summary>
    /// Declare Dead
    /// </summary>
    public void Dead();

    /// <summary>
    /// Respawn item
    /// </summary>
    public void Respawn();
}
