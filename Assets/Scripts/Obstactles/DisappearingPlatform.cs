using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MovingPlatform, IDisappear
{
    [SerializeField] private float m_disappearTimer = 0.0f;
    [SerializeField] private PlatformCollision m_platformCollision = null;

    private bool m_startCountdown = false;
    private void OnEnable()
    {
        m_platformCollision.OnPlayerEnter += StartCountdown;
    }
    private void OnDisable()
    {
        m_platformCollision.OnPlayerEnter -= StartCountdown;
    }
    private void Update()
    {
        if (m_startCountdown)
        {
            if(m_disappearTimer > 0)
            {
                m_disappearTimer -= Time.deltaTime;
            }
            else if(m_disappearTimer <= 0)
            {
                Disappear();
            }
        }
    }
    private void StartCountdown()
    {
        m_startCountdown = true;
    }

    #region IDisappear Implementation
    public void Disappear()
    {
        m_platformCollision.ForceRemoveParent();
        gameObject.SetActive(false);
    }
    #endregion
}
