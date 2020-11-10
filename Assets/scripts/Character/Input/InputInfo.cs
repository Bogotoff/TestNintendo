using UnityEngine;

public class InputInfo : MonoBehaviour
{
    private Vector2 _inputDirection;

    public Vector2 m_inputDirection
    {
        get => _inputDirection;
        set
        {
            _inputDirection = value;
            _inputDirection.ClampMagnitude(1);
        }
    }
}
