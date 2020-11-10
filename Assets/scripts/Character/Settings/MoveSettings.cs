using UnityEngine;

[CreateAssetMenu(fileName = "MoveSettings", menuName = "Settings/MoveSettings", order = 0)]
public class MoveSettings: ScriptableObject
{
    public float m_acceleration = 1;
    public float m_maxSpeed = 1000;
    
    [Range(0f, 1f)]
    public float m_friction = 0;
}