using UnityEngine;

public class GenericVariable<T> : ScriptableObject
{
     [Multiline, SerializeField] private string DeveloperDescription = "";

    /// <summary>
    /// Generic default value
    /// </summary>
    [SerializeField] private T _defaultValue;

    /// <summary>
    /// Generic Original Value
    /// This is the modified value a copy of the default value so base value didn't modified
    /// </summary>
    private T _originalValue;

    private void OnEnable()
    {
        _originalValue = _defaultValue;
    }

    /// <summary>
    /// Value of the generic variable
    /// </summary>
    public T Value
    {
        get
        {
            return _originalValue;
        }
        set
        {
            _originalValue = value;
        }
    }
}