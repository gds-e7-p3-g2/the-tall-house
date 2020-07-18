using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
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