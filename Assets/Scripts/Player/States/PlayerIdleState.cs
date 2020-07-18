using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    class PlayerIdleState : PlayerState
    {
        private Rigidbody2D rigidbody2D;
        private Vector3 m_Velocity = Vector3.zero;

        public PlayerIdleState(PlayerController playerController) : base(playerController) { }

        public override void Enter()
        {
            rigidbody2D = PlayerController.GetComponent<Rigidbody2D>();
            PlayerController.cameraController.ShowFrame();
            PlayerController.PlayerAnimation.GetComponent<Animator>().speed = 0;
        }
        public override void OnUpdate()
        {
            if (InputManager.Horizontal != 0.0f)
            {
                PlayerController.SetState(new PlayerWalkingState(PlayerController));
            }
            if (InputManager.Recording)
            {
                PlayerController.SetState(new PlayerIdleRecordingState(PlayerController));
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

    class PlayerIdleRecordingState : PlayerIdleState
    {
        public PlayerIdleRecordingState(PlayerController playerController) : base(playerController) { }
        public override void OnUpdate()
        {
            if (InputManager.Horizontal != 0.0f)
            {
                PlayerController.SetState(new PlayerWalkingRecordingState(PlayerController));
            }
            if (!InputManager.Recording)
            {
                PlayerController.SetState(new PlayerIdleState(PlayerController));
            }
        }
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
    }

    class PlayerIdleInDangerState : PlayerIdleState
    {
        public PlayerIdleInDangerState(PlayerController playerController) : base(playerController) { }
        public override void ToggleHiding() { }

    }

    class PlayerIdleInDangerRecordingState : PlayerIdleState
    {
        public PlayerIdleInDangerRecordingState(PlayerController playerController) : base(playerController) { }
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
        public override void ToggleHiding() { }
    }
}