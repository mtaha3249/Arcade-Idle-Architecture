using UnityEngine;
using UnityEngine.UI;

public class MarketPurchaseSuccessfull : MonoBehaviour, IGenericCallback
{
    [SerializeField, Multiline(3)] private string _developerInstruction;
    private ResourceOutput _resourceOutput;
    private Button _button;
    [SerializeField] private GameEvent _onMoneyTransfer, _resourceCollected;

    public void OnEventRaisedCallback(params object[] param)
    {
        // core logic of the game on purchase
        // check if the amount collected left is will provide whole number then disable button etc
        _resourceOutput = (ResourceOutput) param[0];
        _button = (Button) param[1];
        _button.interactable = false;
        int outputPrice = (int) (_resourceOutput.CurrentValue * _resourceOutput._currency._currencyAmount);
        _resourceOutput.CurrencyValue = _resourceOutput.CurrencyValue + outputPrice;
        Debug.Log(outputPrice + _resourceOutput._currency._currencyUI.name + " is given.");
        _resourceOutput.CurrentValue = 0;
        // On Money Transfer Successful
        // Must be called after money transfer
        _onMoneyTransfer.Raise();
        _resourceCollected.Raise();
    }
}