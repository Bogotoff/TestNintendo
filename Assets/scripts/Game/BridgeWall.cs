using UnityEngine;
using Zenject;

public class BridgeWall : MonoBehaviour
{
    [Inject]
    private GameStateInfo _gameStateInfo = null;

    private void Awake()
    {
        _gameStateInfo.m_onStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        _gameStateInfo.m_onStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(GameStateInfo.State newState)
    {
        if (newState != GameStateInfo.State.IntroAnimating)
        {
            Destroy(gameObject);
        }
    }
}
