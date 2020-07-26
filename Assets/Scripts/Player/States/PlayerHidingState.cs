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
            PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().color = Color.black;
            PlayerController.PlayerAnimation.GetComponent<Animator>().speed = 0;
            PlayerController.WhatGhostsSee.SetActive(false);
        }
        public override void Exit()
        {
            PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().color = Color.white;
            PlayerController.WhatGhostsSee.SetActive(true);
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