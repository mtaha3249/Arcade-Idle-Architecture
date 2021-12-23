using UnityEngine;

public class PlayerMotor : PlayerMovement
{
    [SerializeField] private GenericReference<float> _turnSpeed;
    [SerializeField] private GenericReference<float> _moveSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Calculate direction to move
    /// </summary>
    /// <param name="_h">horizontal axis value</param>
    /// <param name="_v">vertical axis value</param>
    /// <returns>angle to look</returns>
    float CalculateDirection(float _h, float _v)
    {
        return Mathf.Rad2Deg * (Mathf.Atan2(_h, _v));
    }

    /// <summary>
    /// Rotate player to given axis
    /// </summary>
    /// <param name="_h">horizontal axis value</param>
    /// <param name="_v">vertical axis value</param>
    void Rotate(float _h, float _v)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(Vector3.up * CalculateDirection(_h, _v)), _turnSpeed.Value * Time.deltaTime);
    }

    /// <summary>
    /// Move player forward
    /// </summary>
    /// <param name="_v">vertical axis</param>
    void MoveForward(float _h, float _v)
    {
        rb.velocity = transform.forward * _moveSpeed.Value * Time.deltaTime * (Mathf.Abs(_v) + Mathf.Abs(_h)) * rb.mass;
    }


    /// <summary>
    /// Move and Rotate player to given axis
    /// </summary>
    /// <param name="h">horizontal axis</param>
    /// <param name="v">vertical axis</param>
    public void Move(float h, float v)
    {
        MoveForward(h, v);
        Rotate(h, v);
    }
}