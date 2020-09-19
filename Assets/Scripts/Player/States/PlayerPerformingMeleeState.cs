using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    class PlayerPerformingMeleeState : PlayerState
    {

        public PlayerPerformingMeleeState(PlayerController playerController) : base(playerController) { }

        public override void Enter()
        {
            PlayerController.IsRecording = false;
            PlayerController.MeleeWeapon.OnReady.AddListener(GoToIdle);
            PlayerController.MeleeWeapon.Shoot();
        }
        public override void Exit()
        {
            PlayerController.MeleeWeapon.OnReady.RemoveListener(GoToIdle);
        }

        private void GoToIdle()
        {
            PlayerController.SetState(new PlayerIdleState(PlayerController));
        }
    }
}