using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 40f;
    public float runSpeed = 80f;
    [SerializeField] FlipController flipController;
    [SerializeField] GameObject cameraFrame;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    [SerializeField] PlayerHiding playerHiding;

    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    float horizontalMove = 0f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
    }

    void Walk()
    {
        if (playerHiding.GetIsHiding())
        {
            return;
        }
        Move(horizontalMove * walkSpeed * Time.fixedDeltaTime);
    }

    void Run()
    {
        if (playerHiding.GetIsHiding())
        {
            return;
        }
        float delta = horizontalMove * runSpeed * Time.fixedDeltaTime;
        Move(delta);
        flipController.Face(delta < 0, 2);
        cameraFrame.SetActive(false);
    }

    void FixedUpdate()
    {
        if (playerHiding.GetIsHiding())
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else
        {
            cameraFrame.SetActive(true);
            flipController.Clear();
            Walk();
        }
    }

}