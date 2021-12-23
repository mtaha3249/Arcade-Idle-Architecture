using DG.Tweening;
using UnityEngine;

public class OnRegenerate : MonoBehaviour
{
    public float tweenTime;
    public Ease _easeType;
    public void Tween()
    {
        Vector3 _currentScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(_currentScale, tweenTime).SetEase(_easeType);
    }
}