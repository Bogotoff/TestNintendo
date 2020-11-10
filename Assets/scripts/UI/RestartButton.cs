using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public string m_levelName;

    public void OnClick()
    {
        SceneManager.LoadScene(m_levelName);
    }
}
