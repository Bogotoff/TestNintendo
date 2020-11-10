using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RemainingTimeView : MonoBehaviour
{
    public Text m_text;

    [Inject]
    private TimerCounter _timerCounter;

    private int _prevRemaining = -1;

    private void Update()
    {
        int remainingSeconds = (int)_timerCounter.m_timeRamaining;
        if (remainingSeconds < 0)
        {
            remainingSeconds = 0;
        }
        
        if (remainingSeconds != _prevRemaining)
        {
            _prevRemaining = remainingSeconds;
            m_text.text = _prevRemaining.ToString();
        }
    }
}
