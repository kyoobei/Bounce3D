using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform m_targetPosition = null;
    [SerializeField] private PlatformCollision m_platformCollision = null;
    private Player m_player;
    private void OnEnable()
    {
        m_platformCollision.OnPlayerEnter += UpdatePlayerPosition;
    }
    private void OnDisable()
    {
        m_platformCollision.OnPlayerEnter -= UpdatePlayerPosition;
    }
    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void UpdatePlayerPosition()
    {
        m_player.UpdateSavePosition(m_targetPosition.position);
    }
}
