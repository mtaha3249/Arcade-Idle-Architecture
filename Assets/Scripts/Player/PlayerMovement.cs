using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerState playerState;

    public PlayerState PlayerState
    {
        get { return playerState; }
        set
        {
            playerState = value;
            if (OnPlayerStateChange != null)
                OnPlayerStateChange(playerState);
        }
    }

    public delegate void StateChange(PlayerState playerState);

    public static StateChange OnPlayerStateChange;
}