using UnityEngine;

[RequireComponent(typeof(MoveController))]
public abstract class MovableCharacter : MonoBehaviour
{
    private MoveController _moveController;

    protected MoveController m_moveController
    {
        get
        {
            return _moveController != null ? _moveController : _moveController = GetComponent<MoveController>();
        }
    }
    
    public void SetMovingEnable(bool value)
    {
        m_moveController.enabled = value;
    }
}
