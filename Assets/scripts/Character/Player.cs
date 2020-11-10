using UnityEngine;

public class Player : MovableCharacter
{
    [SerializeField]
    private PlayerConfigs _playerConfigs = null;
    
    private void Awake()
    {
        if (_playerConfigs == null)
        {
            Debug.LogError("_playerConfigs == null", gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_playerConfigs != null)
        {
            Gizmos.DrawWireSphere(transform.position, _playerConfigs.m_affectRadius);
        }
    }

    public float GetAffectRadius()
    {
        return _playerConfigs.m_affectRadius;
    }
}
