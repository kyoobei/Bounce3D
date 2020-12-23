using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    public Action OnPressJump;
    public Action OnPressChange;
    public float Horizontal => m_horizontal;
    public float Vertical => m_vertical;
    private float m_horizontal;
    private float m_vertical;
    private void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPressJump?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnPressChange?.Invoke();
        }
    }
}
