using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerController playerController) : base(playerController, playerController.walkSpeed) { }
        public override void Enter()
        {
            base.Enter();
            PlayerController.cameraController.ShowFrame();
            PlayerController.animationController.SetWalk();
        }
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            PlayerController.LookAtCamera();
            CheckRecording();
            CheckRunning();
        }
        public override void FlipX(bool flipX)
        {
            PlayerController.flipX = flipX;
        }

        public virtual void CheckRunning()
        {
            if (PlayerController.IsRecording)
            {
                return;
            }
            if (InputManager.Run)
            {
                PlayerController.SetState(new PlayerRunningState(PlayerController));
            }
        }

        public virtual void CheckRecording()
        {
            PlayerController.IsRecording = InputManager.Recording;
        }
        public override void PerformMelee()
        {
            if (PlayerController.IsRecording)
            {
                return;
            }
            PlayerController.SetState(new PlayerPerformingMeleeState(PlayerController));
        }
    }
}