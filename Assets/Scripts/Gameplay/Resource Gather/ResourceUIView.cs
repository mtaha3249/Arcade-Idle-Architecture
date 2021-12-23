using UnityEngine;
using UnityEngine.UI;

public class ResourceUIView : MonoBehaviour, IGenericCallback
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _collected;
    public GenericReference<int> _resourceOutput;
    public ResourceUI _resourceUI;

    private string _ItemKey => string.Format("{0}_{1}", _resourceUI._name, _resourceUI.InternalID);

    private void Start()
    {
        _icon.sprite = _resourceUI._icon;
        UpdateCurrencyUI(_resourceOutput.Value);
    }

    /// <summary>
    /// Update Resource value
    /// </summary>
    /// <param name="outputValue">resource amount</param>
    public void UpdateResourceOutput(int outputValue)
    {
        _resourceOutput.Value = outputValue;
    }

    /// <summary>
    /// Update UI can be called externally
    /// </summary>
    /// <param name="outputValue">price output Value</param>
    /// <param name="useAbbriviate">change to K,M,etc</param>
    public void UpdateCurrencyUI(int outputValue, bool useAbbriviate = false)
    {
        _collected.text = useAbbriviate ? Utility.AbbreviateNumber(outputValue) : outputValue.ToString();
    }

    /// <summary>
    /// Update text UI base on amount upgraded
    /// </summary>
    /// <param name="param"></param>
    public void OnEventRaisedCallback(params object[] param)
    {
        UpdateCurrencyUI(_resourceOutput.Value, true);
        SaveLoad.Save(_ItemKey, _resourceOutput.Value);
    }
}