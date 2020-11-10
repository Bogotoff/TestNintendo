using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InputInfo))]
[RequireComponent(typeof(Penguin))]
[RequireComponent(typeof(CharacterAnimController))]
public class PenguinAI : MonoBehaviour
{
    public enum State
    {
        RunningAway,
        WalkToRandom,
        Stay,
        Collected
    }

    [SerializeField]
    private PenguinSettings _penguinSettings = null;
    
    [Inject]
    private GameInfo _gameInfo = null;
    
    [Inject]
    private GameStateInfo _gameStateInfo = null;
    
    private InputInfo _inputInfo;
    private CharacterAnimController _animController;
    private Transform _transform;
    private Vector3 _direction;
    private Vector3 _targetDirection;
    private Vector3 _targetPoint;
    private float _switchTargetTime;
    private State _state = State.WalkToRandom;

    private void Awake()
    {
        _inputInfo = GetComponent<InputInfo>();
        _animController = GetComponent<CharacterAnimController>();
        _transform = transform;
    }

    private void OnEnable()
    {
        if (_penguinSettings == null)
        {
            Debug.LogError("_penguinSettings == null", gameObject);
            enabled = false;
            return;
        }
        
        SwitchTarget();
    }

    private void Update()
    {
        if (_state == State.Collected)
        {
            MoveToExit();
            return;
        }

        if (Time.time > _switchTargetTime)
        {
            if (_state == State.WalkToRandom)
            {
                _switchTargetTime = Time.time + Random.Range(_penguinSettings.m_idleTimeMin, _penguinSettings.m_idleTimeMax);
                _state = State.Stay;
            }
            else
            {
                SwitchTarget();
            }
        }

        var pos = _transform.position.ProjectPlaneY();
        bool isNearPlayer = false;
        foreach (var player in _gameInfo.m_players)
        {
            Vector3 offset = (pos - player.transform.position.ProjectPlaneY());
            float sqrDist = offset.sqrMagnitude;
            float affectRadius = player.GetAffectRadius();
            if (sqrDist < affectRadius * affectRadius && sqrDist > 0.001)
            {
                isNearPlayer = true;
                _state = State.RunningAway;
                _targetDirection += offset.normalized * 3;//умножаем на 3 чтобы влияние игроков было сильнее чем начальное направление
            }
        }

        if (isNearPlayer)
        {
            _state = State.RunningAway;
            _switchTargetTime = Time.time + Random.Range(_penguinSettings.m_runAwayTimeMin, _penguinSettings.m_runAwayTimeMax);
        }

        if (_state == State.Stay)
        {
            _targetDirection = Vector3.zero;
            _direction = Vector3.zero;
        }
        
        Debug.DrawRay(_transform.position, _direction.ProjectPlaneY(), Color.white);
        
        _targetDirection.Normalize();
        _direction = Vector3.Lerp(_direction, _targetDirection, _penguinSettings.m_rotationSpeed * Time.deltaTime);

        if (_state == State.WalkToRandom)
        {
            _direction *= _penguinSettings.m_walkSpeed;
            _animController.m_runWalkBlend = 1;
        }
        else
        {
            _animController.m_runWalkBlend = 0;
        }
        
        _inputInfo.m_inputDirection = _direction.XZ();
    }

    private void MoveToExit()
    {
        _inputInfo.m_inputDirection = Vector2.down;
    }

    private void SwitchTarget()
    {
        _state = State.WalkToRandom;
        
        _targetPoint.x = Random.Range(_penguinSettings.m_targetPointArea.x, _penguinSettings.m_targetPointArea.xMax);
        _targetPoint.z = Random.Range(_penguinSettings.m_targetPointArea.y, _penguinSettings.m_targetPointArea.yMax);

        _targetPoint = Vector3.MoveTowards(_transform.position.ProjectPlaneY(), _targetPoint.ProjectPlaneY(), _penguinSettings.m_walkDistance);
        _targetDirection = (_targetPoint - _transform.position).ProjectPlaneY().normalized;
        _switchTargetTime = Time.time + Random.Range(_penguinSettings.m_targetSwitchCooldownMin, _penguinSettings.m_targetSwitchCooldownMax);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_gameStateInfo.m_state == GameStateInfo.State.IntroAnimating)
        {
            return;
        }
        
        if (other.GetComponent<ExitArea>() != null)
        {
            _targetPoint = other.transform.position.XZ();
            _switchTargetTime = 3;
        }
        else if (other.GetComponent<BridgeTrigger>())
        {
            _state = State.Collected;
            _gameInfo.RemovePenguin(GetComponent<Penguin>());
        }
    }
}
