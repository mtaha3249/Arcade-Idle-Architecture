using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Market Manager
/// Handles exchange and inherit Base Purchase Manager Behavior
/// </summary>
[RequireComponent(typeof(MarketUIManager))]
public class MarketManager : BasePurchaseManager<ResourceOutput, Button>
{
    private void Awake()
    {
        Init();
    }
}
