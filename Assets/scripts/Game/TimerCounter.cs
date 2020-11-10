using UnityEngine;
using Zenject;

public class TimerCounter : ITickable
{
    private float _timeRamaining;
    public float m_timeRamaining => _timeRamaining;

    [Inject]
    private GameStateInfo _gameStateInfo = null;

    public TimerCounter(float totalTime)
    {
        _timeRamaining = totalTime;
    }
    
    public void Tick()
    {
        if (_gameStateInfo.m_state == GameStateInfo.State.Playing)
        {
            _timeRamaining -= Time.deltaTime;
        }
    }
}
