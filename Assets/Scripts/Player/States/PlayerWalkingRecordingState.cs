using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
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