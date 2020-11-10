using UnityEngine;

[RequireComponent(typeof(InputInfo))]
public class PlayerInput : MonoBehaviour
{
    public string m_horizontalAxisName = "Horizontal";
    public string m_verticalAxisName = "Vertical";
    
    private InputInfo _inputInfo;
    
    private void Awake()
    {
        _inputInfo = GetComponent<InputInfo>();
    }

    private void Update()
    {
        var x = Input.GetAxis(m_horizontalAxisName);
        var y = Input.GetAxis(m_verticalAxisName);
        
        _inputInfo.m_inputDirection = new Vector2(x, y)*10;
    }
}
