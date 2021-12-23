using UnityEngine;

public class TreeHealth : Health
{
    [Header("Trees")] public GameObject _mesh;

    public override void Dead()
    {
        base.Dead();
        _mesh.SetActive(false);
    }

    public override void Respawn()
    {
        base.Respawn();
        _mesh.SetActive(true);
    }
}