using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event-TYPE", menuName = "SO-Architecture/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline(5), SerializeField] string _developerInstruction = "";
#endif
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventListener> eventListeners =
        new List<GameEventListener>();

    /// <summary>
    /// Raise Event with given parameters
    /// </summary>
    /// <param name="obj">parameters to raise</param>
    public void Raise(params object[] obj)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(obj);
    }

    /// <summary>
    /// Register Listeners on Enable
    /// </summary>
    /// <param name="listener">Listener to register</param>
    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    /// <summary>
    /// UnRegister Listeners On Disable
    /// </summary>
    /// <param name="listener">Listener to unregister</param>
    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}