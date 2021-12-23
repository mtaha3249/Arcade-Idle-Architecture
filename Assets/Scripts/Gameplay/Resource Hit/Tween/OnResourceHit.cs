using DG.Tweening;
using UnityEngine;

public class OnResourceHit : MonoBehaviour
{
    public float duration;
    public Vector3 strength = Vector3.right;
    public int vibration = 10;
    public float randomness = 90;
    public bool fadeOut = true;

    [Header("Vibration")]
    public bool useVibration;

    public void StartFX()
    {
        Tween();
        if(useVibration)
            Vibration.VibrateLight();
    }
    
    void Tween()
    {
        Quaternion _currentValue = transform.rotation;
        transform.DOShakeRotation(duration, strength, vibration, randomness, fadeOut).OnComplete(() =>
        {
            transform.DORotate(_currentValue.eulerAngles, 0.1f);
        });
    }
}
