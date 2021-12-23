using UnityEngine;

[DefaultExecutionOrder(-20)]
public class DBReader : MonoBehaviour
{
    [SerializeField] private EconomyData Data;
    [SerializeField, Multiline(10)] private string json;

    [Header("Remote Config"), SerializeField]
    private RemoteConfigSO _remoteConfig;

    [SerializeField] private TextAsset _defaultEconomy;

    private void Awake()
    {
        _remoteConfig._economy._defaultValue = _defaultEconomy.text;
        // Fetch JSON from Remote Config
        // json = RemoteConfigValue
        Data = Init(json);
        Data.PushDataToLocalDB();
    }

    /// <summary>
    /// Initialize the Database
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public EconomyData Init(string json)
    {
        return JsonUtility.FromJson<EconomyData>(json);
    }
}