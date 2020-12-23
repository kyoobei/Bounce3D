using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDisappear
{
    [SerializeField] private float m_ungroundedTimer = 5.0f;
    private PlayerMovement m_playerMovement = null;
    [SerializeField] private Vector3 m_savedPlayerPosition = Vector3.zero;
    private float m_unGroundedTimerHolder = 0.0f;
    private void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        Time.timeScale = 1;
        m_unGroundedTimerHolder = m_ungroundedTimer;
        m_savedPlayerPosition = transform.position;
    }
    private void Update()
    {
        if (m_playerMovement.IsGrounded())
        {
            m_ungroundedTimer = m_unGroundedTimerHolder;
        }
        else
        {
            if(m_ungroundedTimer > 0)
            {
                m_ungroundedTimer -= Time.deltaTime;
            }
            else if(m_ungroundedTimer <= 0)
            {
                Disappear();
            }
        }
    }

    public void UpdateSavePosition(Vector3 savedPosition)
    {
        m_savedPlayerPosition = savedPosition;
    }
    public void Disappear()
    {
        m_ungroundedTimer = m_unGroundedTimerHolder;
        m_playerMovement.ResetVelocity();
        transform.position = m_savedPlayerPosition;
    }
}
