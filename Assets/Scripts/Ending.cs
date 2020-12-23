using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] GameObject m_endingUI;
    [SerializeField] private PlatformCollision m_platformCollision = null;
    private void OnEnable()
    {
        m_platformCollision.OnPlayerEnter += DisplayUI;
    }
    private void OnDisable()
    {
        m_platformCollision.OnPlayerEnter -= DisplayUI;
    }
    private void DisplayUI()
    {
        Time.timeScale = 0;
        m_endingUI.SetActive(true);
    }
}
