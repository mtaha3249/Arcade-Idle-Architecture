using UnityEngine;

[RequireComponent(typeof(MarketUIManager))]
public class OpenMarketUIManager : MonoBehaviour, IGenericCallback
{
    [SerializeField, Multiline] private string developerInstruction;

    public GameObject _marketUI;

    private MarketUIManager _marketUIManager;
    private bool isOpened;

    private void Awake()
    {
        _marketUIManager = GetComponent<MarketUIManager>();
        _marketUI.SetActive(false);
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        isOpened = (bool) param[0];
        _marketUI.SetActive(isOpened);
        
        if (isOpened)
            _marketUIManager.OnVisible();
    }
}