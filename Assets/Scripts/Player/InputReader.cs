using UnityEngine;

[RequireComponent(typeof(Joystick))]
public class InputReader : MonoBehaviour
{
    [SerializeField] private GenericReference<Vector2> _inputParser;
    private Joystick _joystick;

    private Vector2 _input = Vector2.zero;
    
    private void Start()
    {
        _inputParser.Value = Vector2.zero;
        _joystick = GetComponent<Joystick>();
    }

    private void Update()
    {
        _input.x = _joystick.Horizontal;
        _input.y = _joystick.Vertical;

        _inputParser.Value = _input;
    }
}
