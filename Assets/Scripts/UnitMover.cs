using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class UnitMover : MonoBehaviour
{
    public InputAction m_inputMove;
    public Vector2 m_vec2MoveValue;

    public Vector3 dir;

    public float m_fCameraHorizontal;
    public float m_fCameraXValue;

    private Rigidbody m_rb;

    private float m_fMoveSpeed = 3.0f;

    public bool can_Move;

    [SerializeField] private CinemachineFreeLook freelook;

    private Animator m_animator;

    private void OnEnable()
    {
        m_inputMove.Enable();
    }
    private void OnDisable()
    {
        m_inputMove.Disable();
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(can_Move == true)
        {
            dir = transform.forward;
            m_vec2MoveValue = m_inputMove.ReadValue<Vector2>();
        }
        else 
        {
            dir = Vector3.zero;
            m_vec2MoveValue = Vector2.zero;
        }
        
    }
    void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * m_vec2MoveValue.y + Camera.main.transform.right * m_vec2MoveValue.x;

        m_rb.velocity = moveForward * m_fMoveSpeed + new Vector3(0, m_rb.velocity.y, 0);

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        if (m_animator != null)
        {
            m_animator.SetBool("run", 0 < m_vec2MoveValue.sqrMagnitude);
        }


        //freelook.m_XAxis.Value = cameraHorizontal;
        //freelook.m_XAxis.m_InputAxisValue = m_fCameraHorizontal;
        //m_fCameraXValue = freelook.m_XAxis.Value;
    }
}
