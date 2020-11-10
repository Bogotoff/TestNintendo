using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Zenject;

public class GameLogic : MonoBehaviour
{
    public PlayableDirector m_startDirector;
    public PlayableDirector m_gameOverDirector;
    
    [Inject]
    private GameStateInfo _gameStateInfo = null;
    
    [Inject]
    private GameInfo _gameInfo = null;

    [Inject]
    private TimerCounter _timerCounter;

    public string m_gameOverSceneName = "GameOverScene";

    private void Awake()
    {
        if (m_startDirector == null || m_gameOverDirector == null)
        {
            Debug.LogError("m_startDirector == null || m_gameOverDirector == null", gameObject);
        }

        m_startDirector.enabled = false;
        m_gameOverDirector.enabled = false;
        
        _gameStateInfo.m_onStateChanged += OnGameStateChanged;
        
        OnGameStateChanged(_gameStateInfo.m_state);
        foreach (var penguin in _gameInfo.m_penguins)
        {
            penguin.SetMovingEnable(true);
        }
        
        _gameInfo.m_onRemovePenguin += OnPenguinCollected;
    }

    private void Start()
    {
        m_startDirector.enabled = true;
    }

    private void OnDestroy()
    {
        _gameStateInfo.m_onStateChanged -= OnGameStateChanged;
    }
    
    private void OnGameStateChanged(GameStateInfo.State newState)
    {
        Debug.Log("OnGameStateChanged: " + newState);
        
        foreach (var player in _gameInfo.m_players)
        {
            player.SetMovingEnable(newState == GameStateInfo.State.Playing);
        }

        if (newState == GameStateInfo.State.GameOver)
        {
            m_gameOverDirector.enabled = true;
        }
    }

    private void Update()
    {
        if (_gameStateInfo.m_state == GameStateInfo.State.Playing && _timerCounter.m_timeRamaining <= 0)
        {
            _gameStateInfo.m_state = GameStateInfo.State.GameOver;
        }
    }

    public void OnIntroCompleted()
    {
        m_startDirector.enabled = false;
        _gameStateInfo.m_state = GameStateInfo.State.Playing;
    }

    public void OnPenguinCollected(Penguin penguin)
    {
        if (_gameStateInfo.m_state == GameStateInfo.State.GameOver)
        {
            return;
        }
        
        if (_gameInfo.m_penguins.Count == 0)
        {
            _gameStateInfo.m_state = GameStateInfo.State.GameOver;
        }
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(m_gameOverSceneName, LoadSceneMode.Single);
    }
}
