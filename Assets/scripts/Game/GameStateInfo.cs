using System;

public class GameStateInfo
{
    public enum State
    {
        IntroAnimating,
        Playing,
        GameOver
    }
    
    private State _state = State.IntroAnimating;
    
    public Action<State> m_onStateChanged;
    
    public State m_state
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                m_onStateChanged?.Invoke(_state);
            }
        }
    }
}
