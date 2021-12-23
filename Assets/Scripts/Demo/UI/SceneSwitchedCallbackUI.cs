using UnityEngine;

public class SceneSwitchedCallbackUI : MonoBehaviour
{
    public GameState _stateToAct;
    public GameObject _bg;
    public void OnSceneSwitched(object[] obj)
    {
        GameState _currentState = (GameState) obj[0];
        // disable this panel
        _bg.SetActive(false);
        
        if (_currentState == _stateToAct)
        {
            _bg.SetActive(true);
        }
    }
}
