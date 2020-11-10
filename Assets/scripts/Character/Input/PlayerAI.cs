using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InputInfo))]
public class PlayerAI : MonoBehaviour
{
    public float m_switchTargetCooldown = 4;
    
    [Inject]
    private GameInfo _gameInfo = null;
    
    private InputInfo _inputInfo;
    private Penguin _targetPenguin;
    private float _switchTargetTime;
    private Transform _transform;
    
    private void Awake()
    {
        _inputInfo = GetComponent<InputInfo>();
        _transform = transform; //Cache
        _gameInfo.m_onRemovePenguin += OnRemovePenguin;
    }

    private void OnDestroy()
    {
        _gameInfo.m_onRemovePenguin -= OnRemovePenguin;
    }

    private void OnRemovePenguin(Penguin penguin)
    {
        if (_targetPenguin == penguin)
        {
            SwitchTarget();
        }
    }

    private void Update()
    {
        if (Time.time >= _switchTargetTime)
        {
            SwitchTarget();
        }
        
        if (_targetPenguin == null)
        {
            return;
        }

        var offset = _targetPenguin.transform.position - _transform.position;
        _inputInfo.m_inputDirection = offset.XZ();
    }

    private void SwitchTarget()
    {
        if (_gameInfo.m_penguins.Count == 0)
        {
            _targetPenguin = null;
            return;
        }

        _targetPenguin = _gameInfo.m_penguins[Random.Range(0, _gameInfo.m_penguins.Count)];
        _switchTargetTime = Time.time + m_switchTargetCooldown;
    }
}
