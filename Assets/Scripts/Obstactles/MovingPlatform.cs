﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    protected const float LIMIT_PLATFORM_VALUE = 1f;
    
    [SerializeField] protected Vector3 m_platformDirectionNormalized = Vector3.zero;
    [SerializeField] protected Vector3 m_maxClampDirection = Vector3.zero;
    [SerializeField] protected float m_platformSpeed;
    protected Rigidbody m_rigidBody;
    protected Vector3 m_initialPosition = Vector3.zero;
    protected Vector3 m_maxPosition = Vector3.zero;
    private bool m_hasReached = false;

    #region UNITY METHOD
    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        m_platformDirectionNormalized = m_platformDirectionNormalized.normalized;
        m_initialPosition = transform.localPosition;
        m_maxPosition = new Vector3(m_initialPosition.x, m_initialPosition.y, m_initialPosition.z);
        m_maxPosition += m_maxClampDirection;
    }
    protected void FixedUpdate()
    {
        MoveInLoop();
    }
    #endregion

    #region USER DEFINED METHODS
    protected virtual void MoveInLoop()
    {
        if (!m_hasReached)
        {
            float currentDistanceToTarget = Vector3.Distance(transform.localPosition, m_maxPosition);
            if (currentDistanceToTarget > LIMIT_PLATFORM_VALUE)
            {
                Vector3 movementDir = m_platformDirectionNormalized * m_platformSpeed * Time.fixedDeltaTime;
                m_rigidBody.MovePosition(transform.position + movementDir);
            }
            else if(currentDistanceToTarget <= LIMIT_PLATFORM_VALUE)
            {
                m_hasReached = true;
            }
        }
        else if(m_hasReached)
        {
            float currentDistanceToTarget = Vector3.Distance(m_initialPosition, transform.localPosition);
            if (currentDistanceToTarget > LIMIT_PLATFORM_VALUE)
            {
                Vector3 movementDir = -(m_platformDirectionNormalized) * m_platformSpeed * Time.fixedDeltaTime;
                m_rigidBody.MovePosition(transform.position + movementDir);
            }
            else if(currentDistanceToTarget <= LIMIT_PLATFORM_VALUE)
            {
                m_hasReached = false;
            }
        }
    }
    #endregion
}
