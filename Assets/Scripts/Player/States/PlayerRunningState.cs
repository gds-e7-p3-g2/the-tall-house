using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
