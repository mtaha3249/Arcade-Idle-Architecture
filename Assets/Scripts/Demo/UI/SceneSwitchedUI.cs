using UnityEngine;

public class SceneSwitchedUI : MonoBehaviour
{
    public GameState _nextGameState;
    public GameEvent _event;
    public void OnClick()
    {
        _event.Raise(_nextGameState);
    }
}
