using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerIdleState : PlayerState
{
    private Rigidbody2D rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;

    public PlayerIdleState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        rigidbody2D = PlayerController.GetComponent<Rigidbody2D>();
        PlayerController.SetCameraFrameActive(true);
        PlayerController.PlayerAnimation.GetComponent<Animator>().speed = 0;
    }
    public override void OnUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0.0f)
        {
            PlayerController.SetState(new PlayerWalkingState(PlayerController));
        }
        // add other transitions:
        // start recording 
        // get spotted by ghost
    }
    public override void OnFixedUpdate()
    {
        PlayerController.LookAtCamera();
    }


    public override void ToggleHiding()
    {
        PlayerController.SetState(new PlayerHidingState(PlayerController));
    }

    public override void FlipX(bool flipX)
    {
        PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().flipX = flipX;
    }

}

class PlayerHidingState : PlayerState
{
    public PlayerHidingState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        PlayerController.SetCameraFrameActive(false);
        PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().color = Color.black;
        PlayerController.PlayerAnimation.GetComponent<Animator>().speed = 0;
    }
    public override void Exit()
    {
        PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public override void ToggleHiding()
    {
        PlayerController.SetState(new PlayerIdleState(PlayerController));
    }

}

class PlayerMovingState : PlayerState
{
    private Rigidbody2D rigidbody2D;
    private Vector3 VelocityZero = Vector3.zero;
    private float distanceMultiplier = 10f;
    protected float Speed = 0.0f;
    protected float horizontalMove = 0f;

    public PlayerMovingState(PlayerController playerController, float speed) : base(playerController)
    {
        Speed = speed;
    }

    public override void Move(float distance)
    {
        Vector3 targetVelocity = new Vector2(distance * distanceMultiplier * Speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref VelocityZero, 0.0f);
    }

    public override void Enter()
    {
        rigidbody2D = PlayerController.GetComponent<Rigidbody2D>();
    }
    public override void OnUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (horizontalMove == 0.0f)
        {
            PlayerController.SetState(new PlayerIdleState(PlayerController));
        }
    }

    public override void OnFixedUpdate()
    {
        PlayerController.Move(horizontalMove);
    }

}

class PlayerWalkingState : PlayerMovingState
{
    private float AnimationSpeedMemory;
    public PlayerWalkingState(PlayerController playerController) : base(playerController, playerController.walkSpeed) { }
    public override void Enter()
    {
        base.Enter();
        PlayerController.SetCameraFrameActive(true);
        PlayerController.PlayerAnimation.GetComponent<Animator>().speed = 1;
    }

    public override void Exit()
    {
        PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", 1.0f);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        PlayerController.LookAtCamera();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerController.SetState(new PlayerRunningState(PlayerController));
        }


        // is the player going backwards
        if (PlayerController.IsCameraOnLeft() && horizontalMove > 0.0f || !PlayerController.IsCameraOnLeft() && horizontalMove < 0.0f)
        {
            PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", -1.0f);
        }
        else
        {
            PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", 1.0f);
        }
    }
    public override void FlipX(bool flipX)
    {
        PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().flipX = flipX;
    }
}

class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerController playerController) : base(playerController, playerController.runSpeed) { }

    public override void Enter()
    {
        base.Enter();
        PlayerController.SetCameraFrameActive(false);
        PlayerController.PlayerAnimation.GetComponent<Animator>().speed = 1.5f;
        PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", 1.0f);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            PlayerController.SetState(new PlayerWalkingState(PlayerController));
        }

        if (horizontalMove < 0)
        {
            PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().flipX = true;
            PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", 1.0f);
        }
        else
        {
            PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().flipX = false;
            PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", 1.0f);
        }


    }
}

public class PlayerController : StateMachine<PlayerState>
{
    private bool IsHiding;
    [SerializeField] GameObject cameraFrame;
    public float walkSpeed = 40f;
    public float runSpeed = 80f;
    public GameObject PlayerAnimation;

    public void SetCameraFrameActive(bool cameraFrameActive)
    {
        cameraFrame.SetActive(cameraFrameActive);
    }

    public void Start()
    {
        SetState(new PlayerIdleState(this));
    }
    public void Move(float distance)
    {
        CurrentState.Move(distance);
    }

    void Update()
    {
        CurrentState.OnUpdate();
    }

    void FixedUpdate()
    {
        CurrentState.OnFixedUpdate();
    }

    public void ToggleHiding()
    {
        CurrentState.ToggleHiding();
    }

    public bool IsCameraOnLeft()
    {
        return transform.position.x > cameraFrame.transform.position.x;
    }

    public void LookAtCamera()
    {
        FlipX(IsCameraOnLeft());
    }

    public void FlipX(bool flipX)
    {
        CurrentState.FlipX(flipX);
    }

}
