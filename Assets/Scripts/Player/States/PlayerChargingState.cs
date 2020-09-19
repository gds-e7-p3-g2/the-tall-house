using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    class PlayerChargingState : PlayerState
    {
        public PlayerChargingState(PlayerController playerController) : base(playerController) { }

        public override void Enter()
        {
            PlayerController.cameraController.StartCharging();
            PlayerController.animationController.SetCharging();
        }
        public override void Exit()
        {
            PlayerController.cameraController.StopCharging();
        }

        public override void ToggleCharging()
        {
            PlayerController.SetState(new PlayerIdleState(PlayerController));
        }

        public override bool GetIsCharging()
        {
            return true;
        }
    }
}