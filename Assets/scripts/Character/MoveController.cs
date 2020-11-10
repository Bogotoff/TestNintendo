using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputInfo))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private MoveSettings _moveSettings = null;
    private CharacterController _characterController;
    private InputInfo _inputInfo;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _inputInfo = GetComponent<InputInfo>();
    }

    private void OnEnable()
    {
        if (_moveSettings == null)
        {
            Debug.LogError("_moveSettings == null", gameObject);
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        var moveDir = _inputInfo.m_inputDirection;

        var moveSpeed = new Vector3(moveDir.x, 0, moveDir.y) * _moveSettings.m_acceleration;

        var velocity = _characterController.velocity;
        velocity += moveSpeed * Time.fixedDeltaTime;
        
        ApplyFriction(ref velocity);

        velocity.ClampMagnitude(_moveSettings.m_maxSpeed);

        _characterController.SimpleMove(velocity);
        var pos = _transform.position;
        pos.y = 1;
        _transform.position = pos;
    }

    private void ApplyFriction(ref Vector3 velocity)
    {
        velocity = Vector3.Lerp(velocity, Vector3.zero, _moveSettings.m_friction);
    }
    
    public Vector3 GetVelocity()
    {
        return _characterController.velocity.ProjectPlaneY();
    }
}
