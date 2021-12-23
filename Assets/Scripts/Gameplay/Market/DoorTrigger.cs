using UnityEngine;

public class DoorTrigger : TriggerEvents
{
    public GameEvent _isInTrigger;
    public bool sendMe = true;
    public GameObject MyObject;
    public override void TriggerEnter(GameObject triggeredObject)
    {
        _isInTrigger.Raise(true, sendMe ? MyObject : triggeredObject);
    }

    public override void TriggerExit(GameObject triggeredObject)
    {
        _isInTrigger.Raise(false, sendMe ? MyObject : triggeredObject);
    }
}
