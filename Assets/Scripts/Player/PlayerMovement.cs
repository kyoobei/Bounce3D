using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MAX_ANGULAR_VELOCITY = 15f;
    private const float MIN_MAGNITUDE = 0.1f;
    private const float MIN_GROUND_DISTANCE = 0.1f;

    [SerializeField] private float m_movementSpeed = 0.0f;
    [SerializeField] private float m_jumpPower = 0.0f;
    private PlayerInput m_playerInput;
    private Rigidbody m_rigidBody;
    private Camera m_mainCamera;
    private float m_groundDistance = 0.0f;

    #region UNITY METHODS
    private void Awake()
    {
        m_mainCamera = Camera.main;
        m_playerInput = GetComponent<PlayerInput>();
        m_rigidBody = GetComponent<Rigidbody>();

        m_rigidBody.maxAngularVelocity = MAX_ANGULAR_VELOCITY;
    }
    private void Start()
    {
        m_groundDistance = GetComponent<Collider>().bounds.extents.y;
    }
    private void OnEnable()
    {
        m_playerInput.OnPressJump += Jump;
    }
    private void OnDisable()
    {
        m_playerInput.OnPressJump -= Jump;
    }
    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region USER DEFINED METHODS
    private void Move()
    {
        Vector3 direction = new Vector3(m_playerInput.Horizontal, 0f, m_playerInput.Vertical).normalized;
        if (direction.magnitude >= MIN_MAGNITUDE)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + m_mainCamera.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 movement = moveDirection * m_movementSpeed * Time.fixedDeltaTime;
            m_rigidBody.MovePosition(transform.position + movement);
        }
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            m_rigidBody.AddForce(new Vector3(0, m_jumpPower, 0), ForceMode.Impulse);
        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, m_groundDistance + MIN_GROUND_DISTANCE);
    }
    #endregion
}
