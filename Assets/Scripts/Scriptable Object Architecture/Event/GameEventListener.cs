using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    /// <summary>
    /// Event to Listen
    /// </summary>
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    /// <summary>
    /// Response when event is raised
    /// </summary>
    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<object[]> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    /// <summary>
    /// Calls when Event is Raised with given parameters
    /// Call all responses register in the Unity Event
    /// </summary>
    /// <param name="obj">parameters</param>
    public void OnEventRaised(params object[] obj)
    {
        Response.Invoke(obj);
    }
}
