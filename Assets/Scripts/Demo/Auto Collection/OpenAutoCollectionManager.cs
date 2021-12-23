using UnityEngine;

[RequireComponent(typeof(UpgradeUIManager))]
[RequireComponent(typeof(AutoCollectionUIManager))]
public class OpenAutoCollectionManager : MonoBehaviour
{
    [SerializeField, Multiline] private string developerInstruction;

    public GameObject _collectionUI;

    private UpgradeUIManager _upgradeUIManager;
    private AutoCollectionUIManager _autoCollectionUIManager;
    private bool isOpened;

    private void Awake()
    {
        _upgradeUIManager = GetComponent<UpgradeUIManager>();
        _autoCollectionUIManager = GetComponent<AutoCollectionUIManager>();
        _collectionUI.SetActive(false);
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        isOpened = (bool) param[0];
        _collectionUI.SetActive(isOpened);

        if (isOpened)
        {
            _upgradeUIManager.OnVisible();
            _autoCollectionUIManager.OnVisible();
        }
    }
}
