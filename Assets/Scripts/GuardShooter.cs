using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShooter : MonoBehaviour
{
    [SerializeField] private Pooler m_bulletPooler = null;
    [SerializeField] private Transform m_bulletPosition =  null;
    [SerializeField] private float m_shootingInterval = 0.0f;
    private float m_intervalHolder;
    private void Start()
    {
        m_intervalHolder = m_shootingInterval;
    }
    private void Update()
    {
        if(m_shootingInterval > 0)
        {
            m_shootingInterval -= Time.deltaTime;
        }
        else if(m_shootingInterval <= 0)
        {
            ReleaseBullet();
            m_shootingInterval = m_intervalHolder;
        }
    }
    private void ReleaseBullet()
    {
        GameObject go = m_bulletPooler.GetClone();
        go.transform.position = m_bulletPosition.position;
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.SetPoolerForReturning(m_bulletPooler);
        bullet.Shoot();      
    }
}
