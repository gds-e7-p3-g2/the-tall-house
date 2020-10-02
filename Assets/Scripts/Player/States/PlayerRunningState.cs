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
            PlayerController.animationController.SetRun();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (!InputManager.Run)
            {
                PlayerController.SetState(new PlayerWalkingState(PlayerController));
            }

            PlayerController.flipX = horizontalMove < 0;

            SoundsController.Instance.findSound("Running").Play();
        }
    }
}