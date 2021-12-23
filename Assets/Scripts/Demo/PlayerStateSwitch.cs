using UnityEngine;

public class PlayerStateSwitch : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void OnEventRaised(object[] obj)
    {
        GameState _currentState = (GameState) obj[0];
        switch (_currentState)
        {
            case GameState.MainMenu:
                _playerMovement.PlayerState = PlayerState.MainMenu;
                break;
            case GameState.Gameplay:
                _playerMovement.PlayerState = PlayerState.Running;
                break;
            case GameState.Paused:
                break;
        }
    }
}
