using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
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

        public override void PerformMelee() { }
    }
}