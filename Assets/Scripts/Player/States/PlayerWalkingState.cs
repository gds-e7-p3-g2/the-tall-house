using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
