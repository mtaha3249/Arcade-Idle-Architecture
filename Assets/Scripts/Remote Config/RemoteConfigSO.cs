using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Remote Config", menuName = "Arcade Idle/Remote Config", order = 1)]
public class RemoteConfigSO : BaseSO
{
    [Serializable]
    public class RemoteConfig<T>
    {
        public string _id;
        public T _defaultValue;
    }

    public RemoteConfig<string> _economy;
}