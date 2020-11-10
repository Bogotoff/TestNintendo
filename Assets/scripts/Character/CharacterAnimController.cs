using UnityEngine;

[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(InputInfo))]
public class CharacterAnimController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;
    
    [Tooltip("Предполагаемая максимальная скорость движения")]
    public float m_maxVelocity = 2;
    public float m_speedCoef = 2;
    public float m_runWalkBlend = 0;
    
    private MoveController _moveController;
    private InputInfo _inputInfo;
    private Transform _transform;
    private static readonly int _RUNNING_SPEED = Animator.StringToHash("RunningSpeed");
    private static readonly int _IS_RUNNING = Animator.StringToHash("IsRunning");
    private static readonly int _RUN_WALK_BLEND = Animator.StringToHash("RunWalkBlend");

    private void Awake()
    {
        _transform = transform;
        _moveController = GetComponent<MoveController>();
        _inputInfo = GetComponent<InputInfo>();
    }

    private void OnEnable()
    {
        if (_animator == null)
        {
            Debug.LogError("m_animator == null");
            enabled = false;
        }
    }

    public void Update()
    {
        var dir = _inputInfo.m_inputDirection;
        if (_moveController.enabled && dir.sqrMagnitude > 0.01)
        {
            var normalizedDir = dir.normalized;
            
            var velocity = _moveController.GetVelocity().XZ();

            velocity.ClampMagnitude(m_maxVelocity);
            velocity /= m_maxVelocity;

            //Получаем проекцию от -1 до 1
            var project = Vector2.Dot(velocity, normalizedDir);
            float animSpeedValue = 1 + (1 - project);//от 1 до 3

            _animator.SetBool(_IS_RUNNING, true);
            _animator.SetFloat(_RUNNING_SPEED, animSpeedValue * m_speedCoef);
            
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.LookRotation(new Vector3(normalizedDir.x, 0, normalizedDir.y)), Time.deltaTime * 360*4);
            //_transform.forward = Vector3.Slerp(_transform.forward, new Vector3(normalizedDir.x, 0, normalizedDir.y), 180 * Time.deltaTime);
        }
        else
        {
            _animator.SetBool(_IS_RUNNING, false);
            _animator.SetFloat(_RUNNING_SPEED, 0);
        }
        _animator.SetFloat(_RUN_WALK_BLEND, m_runWalkBlend);
    }
}
