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
            UpdateAnimation();

            SoundsController.Instance.findSound("Walking").Play();
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
            if (InputManager.FlashLoading)
            {
                PlayerController.cameraController.FlashLoadingPressed();
            }
            if (InputManager.FlashLoadingReleased)
            {
                PlayerController.cameraController.FlashLoadingReleased();
            }

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
        protected virtual void UpdateAnimation()
        {
            bool goingBackwards = PlayerController.IsCameraOnLeft() && horizontalMove > 0.0f || !PlayerController.IsCameraOnLeft() && horizontalMove < 0.0f;

            PlayerController.animationController.SetDirection(goingBackwards ? -1f : 1f);
        }
    }
}