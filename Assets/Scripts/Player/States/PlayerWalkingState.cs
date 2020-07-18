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
            PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().flipX = flipX;
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
    }

    class PlayerWalkingRecordingState : PlayerWalkingState
    {
        public PlayerWalkingRecordingState(PlayerController playerController) : base(playerController) { }

        public override void Enter()
        {
            base.Enter();
            PlayerController.cameraController.StartRecording();
        }
        public override void Exit()
        {
            base.Exit();
            PlayerController.cameraController.StopRecording();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            PlayerController.LookAtCamera();
            if (!InputManager.Recording)
            {
                PlayerController.SetState(new PlayerWalkingState(PlayerController));
            }
        }

        public override void CheckRunning() { }

        public override void CheckRecording()
        {
            if (!InputManager.Recording)
            {
                PlayerController.SetState(new PlayerWalkingState(PlayerController));
            }
        }

        protected override void OnStopMoving()
        {
            PlayerController.SetState(new PlayerIdleRecordingState(PlayerController));
        }
    }
}