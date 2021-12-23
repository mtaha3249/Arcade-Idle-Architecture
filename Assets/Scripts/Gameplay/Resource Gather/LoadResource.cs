using UnityEngine;

[RequireComponent(typeof(ResourceUIView))]
public class LoadResource : MonoBehaviour
{
    private string _ItemKey => string.Format("{0}_{1}", GetComponent<ResourceUIView>()._resourceUI._name,
        GetComponent<ResourceUIView>()._resourceUI.InternalID);

    private void Awake()
    {
        GetComponent<ResourceUIView>()._resourceOutput.Value = SaveLoad.Fetch<int>(new SaveKeys.Keys<int>(_ItemKey, 0));
    }
}