using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerController playerController) : base(playerController, playerController.runSpeed) { }

        public override void Enter()
        {
            base.Enter();
            PlayerController.cameraController.HideFrame();
            PlayerController.PlayerAnimation.GetComponent<Animator>().speed = 1.5f;
            PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", 1.0f);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (!InputManager.Run)
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

        protected override void UpdateAnimation()
        {
            // leave empty 
        }
    }

    class PlayerRunningInDangerState : PlayerRunningState
    {
        public PlayerRunningInDangerState(PlayerController playerController) : base(playerController) { }
    }
}