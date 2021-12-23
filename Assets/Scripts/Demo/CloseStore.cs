using UnityEngine;

public class CloseStore : MonoBehaviour, IRaise<bool>
{
    public GameEvent _storeInside;

    public void Raise(bool value)
    {
        _storeInside.Raise(value);
    }
}