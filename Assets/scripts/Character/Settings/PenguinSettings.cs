using UnityEngine;

[CreateAssetMenu(fileName = "PenguinSettings", menuName = "Settings/PenguinSettings", order = 0)]
public class PenguinSettings : ScriptableObject
{
    public float m_idleTimeMin = 0.1f;
    public float m_idleTimeMax = 2;

    public float m_walkSpeed = 0.3f;
    public float m_walkDistance = 3;
    
    public float m_runAwayTimeMin = 0;
    public float m_runAwayTimeMax = 2;
    
    public float m_targetSwitchCooldownMin = 3;
    public float m_targetSwitchCooldownMax = 5;
    
    [Tooltip("Размеры арены(для поиска случайной точки)")]
    public Rect m_targetPointArea = new Rect(0, 0, 100, 100);

    public float m_rotationSpeed = 1;
}
