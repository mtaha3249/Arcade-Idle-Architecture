using UnityEngine;

[RequireComponent(typeof(UpgradeUIManager))]
public class OpenUpgradeUIManager : MonoBehaviour
{
    [SerializeField, Multiline] private string developerInstruction;

    public GameObject _marketUI;

    private UpgradeUIManager _upgradeUIManager;
    private bool isOpened;

    private void Awake()
    {
        _upgradeUIManager = GetComponent<UpgradeUIManager>();
        _marketUI.SetActive(false);
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        isOpened = (bool) param[0];
        _marketUI.SetActive(isOpened);
        
        if (isOpened)
            _upgradeUIManager.OnVisible();
    }
}
