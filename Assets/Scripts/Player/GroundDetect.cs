using System;
using System.Collections;
using UnityEngine;

public interface IUVSet
{
    public bool IsMatched(params Material[] materials);
}

[Serializable]
public struct ColorSet : IUVSet
{
    [SerializeField] private string _colorID;
    [SerializeField] private Color _color;

    public bool IsMatched(params Material[] materials)
    {
        foreach (var material in materials)
        {
            if (material.color == _color)
                return true;
        }

        return false;
    }
}

[Serializable]
public struct TextureSet : IUVSet
{
    [SerializeField] private string _textureID;
    [SerializeField] private Texture2D _texture;

    public bool IsMatched(params Material[] materials)
    {
        foreach (var material in materials)
        {
            if (material.mainTexture == _texture)
                return true;
        }

        return false;
    }
}

public class GroundDetect : MonoBehaviour
{
    public GenericReference<ColorSet>[] _groundColor;
    public GenericReference<TextureSet>[] _groundTexture;
    public GenericReference<float> _checkInterval;
    public GenericReference<bool> _isGrounded;

    public GenericReference<GameObject> _currentPatch;
    private WaitForSeconds _waitForSeconds;
    private RaycastHit _hit;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_checkInterval.Value);
        StartCoroutine(CheckGroundRoutine());
    }

    IEnumerator CheckGroundRoutine()
    {
        while (true)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out _hit, 2))
            {
                Debug.DrawLine(transform.position, _hit.point, Color.red);
                foreach (var _color in _groundColor)
                {
                    if (_hit.transform.GetComponent<Renderer>())
                    {
                        _isGrounded.Value =
                            _color.Value.IsMatched(_hit.transform.GetComponent<Renderer>().materials);
                        _currentPatch.Value = _hit.transform.gameObject;
                    }

                    if (_isGrounded.Value)
                        break;
                }

                foreach (var _texture in _groundTexture)
                {
                    if (_hit.transform.GetComponent<Renderer>())
                    {
                        _isGrounded.Value =
                            _texture.Value.IsMatched(_hit.transform.GetComponent<Renderer>().materials);
                        _currentPatch.Value = _hit.transform.gameObject;
                    }

                    if (_isGrounded.Value)
                        break;
                }
            }

            yield return _waitForSeconds;
        }
    }
}