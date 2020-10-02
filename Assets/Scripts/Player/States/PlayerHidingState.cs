using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    class PlayerHidingState : PlayerState
    {
        public PlayerHidingState(PlayerController playerController) : base(playerController) { }

        public override void Enter()
        {
            PlayerController.cameraController.HideFrame();
            PlayerController.animationController.SetHiding();
            PlayerController.WhatGhostsSee.SetActive(false);
        }

        public override void OnUpdate()
        {
            SoundsController.Instance.findSound("HeartBeat").Play();
        }

        public override void Exit()
        {
            PlayerController.animationController.LeaveHiding();
            PlayerController.WhatGhostsSee.SetActive(true);
        }

        public override void StartBeingStuned()
        {
            // empty
        }

        public override void ToggleHiding()
        {
            PlayerController.SetState(new PlayerIdleState(PlayerController));
        }

        public override bool GetIsHiding()
        {
            return true;
        }

    }
}