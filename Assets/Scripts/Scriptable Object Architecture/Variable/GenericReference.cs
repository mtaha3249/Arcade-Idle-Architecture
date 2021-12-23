using System;
using UnityEngine;

[Serializable]
public class GenericReference<T>
{
    /// <summary>
    /// bool value indicate use constant value
    /// </summary>
    public bool UseConstant = true;
    /// <summary>
    /// Generic constant value
    /// </summary>
    public T ConstantValue;
    /// <summary>
    /// Generic Scriptable object value
    /// </summary>
    public GenericVariable<T> Variable;

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GenericReference()
    {
    }

    /// <summary>
    /// Override Constructor
    /// </summary>
    /// <param name="value">value to assign</param>
    public GenericReference(T value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    /// <summary>
    /// Value of the object
    /// </summary>
    public T Value
    {
        get
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }
        set
        {
            if (UseConstant)
                ConstantValue = value;
            else
                Variable.Value = value;
        }
    }
}