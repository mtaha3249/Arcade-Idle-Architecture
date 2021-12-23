using System;
using UnityEngine;

public class AnimationMotor : MonoBehaviour
{
    [SerializeField] private FloatParameter _moveParameter;
    [SerializeField] private TriggerParameter _triggerParameter;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Move Animator
    /// </summary>
    /// <param name="_h">Horizontal Axis</param>
    /// <param name="_v">Vertical Axis</param>
    public void MoveAnimator(float _h, float _v)
    {
        _animator.SetFloat(_moveParameter.Hash, (Mathf.Abs(_h) + Mathf.Abs(_v)), _moveParameter._smoothTime.Value, Time.deltaTime);
    }
    
    /// <summary>
    /// Player Attack
    /// </summary>
    public void Attack()
    {
        _animator.SetTrigger(_triggerParameter.Hash);
    }
}

#region Parameters Classes

/// <summary>
/// This is a base class of parameter
/// This class help developer for optimization calling of parameter
/// using Hash which is integer and comparatively faster than String
/// </summary>
[Serializable]
public class BaseParameter
{
    /// <summary>
    /// Parameter name in animator
    /// </summary>
    [SerializeField] private GenericReference<string> _parameterName;
    private int _hash = 0;

    /// <summary>
    /// Integer hash value to transition faster for parameter
    /// </summary>
    public int Hash
    {
        get
        {
            if (_hash == 0)
                _hash = Animator.StringToHash(_parameterName.Value);
            return _hash;
        }
    }
}

/// <summary>
/// Float Parameter class, inherited from Base Parameter
/// </summary>
[Serializable]
public class FloatParameter : BaseParameter
{
    /// <summary>
    /// This parameter helps float parameter to lerp smoothly in given time
    /// with the increment of last parameter.
    /// For more information read documentation of animator.SetFloat();
    /// </summary>
    public GenericReference<float> _smoothTime;
}

/// <summary>
/// Trigger Parameter class, inherited from Base Parameter
/// </summary>
[Serializable]
public class TriggerParameter : BaseParameter
{
}

#endregion