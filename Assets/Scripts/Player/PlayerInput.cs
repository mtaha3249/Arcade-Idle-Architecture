using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AnimationMotor))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GenericReference<Vector2> _input;
    [SerializeField] private bool _freezeOnUnHold;
    private PlayerMotor _motor;
    private AnimationMotor _animationMotor;

    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _animationMotor = GetComponent<AnimationMotor>();
        _freezeOnUnHold = true;
    }

    private void FixedUpdate()
    {
        if (_freezeOnUnHold)
        {
            if (Input.GetMouseButton(0))
                _motor.Move(_input.Value.x, _input.Value.y);
        }
        else
        {
            _motor.Move(_input.Value.x, _input.Value.y);
        }

        _animationMotor.MoveAnimator(_input.Value.x, _input.Value.y);
    }
}