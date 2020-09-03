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
            CheckRunning();
            CheckRecording();
        }
        public override void FlipX(bool flipX)
        {
            PlayerController.flipX = flipX;
        }

        public virtual void CheckRunning()
        {
            if (InputManager.Run)
            {
                PlayerController.SetState(new PlayerRunningState(PlayerController));
            }
        }

        public virtual void CheckRecording()
        {
            if (InputManager.Recording)
            {
                PlayerController.SetState(new PlayerWalkingRecordingState(PlayerController));
            }
        }
        public override void PerformMelee()
        {
            PlayerController.MeleeWeapon.Shoot();
        }
    }
}