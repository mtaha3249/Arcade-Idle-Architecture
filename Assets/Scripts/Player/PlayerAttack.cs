using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationMotor))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Detection")] [SerializeField] private GenericReference<float> _attackRange;
    [SerializeField] private GenericReference<LayerMask> _attackMask;

    [Header("Attack")] [SerializeField] private Upgradeable _attackDelay;
    [SerializeField] private Upgradeable _damage;
    private WaitForSeconds _attackDelayWait;
    private AnimationMotor _animation;

    private void Start()
    {
        _animation = GetComponent<AnimationMotor>();
        _attackDelayWait = new WaitForSeconds(_attackDelay.Value);
        StartCoroutine(CheckTarget());
    }

    /// <summary>
    /// Check Target after given interval
    /// Optimized than Update or Fixed Update
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckTarget()
    {
        while (true)
        {
            FindTargets();
            yield return _attackDelayWait;
        }
    }

    /// <summary>
    /// Find Target
    /// </summary>
    void FindTargets()
    {
        Collider[] _colliders = new Collider[10];
        int numColliders =
            Physics.OverlapSphereNonAlloc(transform.position, _attackRange.Value, _colliders, _attackMask.Value);
        for (int i = 0; i < numColliders; i++)
        {
            if (Vector3.Dot(transform.position, _colliders[i].transform.position) > 0.5f)
            {
                _animation.Attack();
                if (_colliders[i].GetComponentInParent<IHealth>() != null)
                {
                    IHealth _health = _colliders[i].GetComponentInParent<IHealth>();
                    _health.GetDamage(_damage.Value);
                }
            }
        }
    }

    /// <summary>
    /// Draw Gizmos on the item
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange.Value);
    }
}