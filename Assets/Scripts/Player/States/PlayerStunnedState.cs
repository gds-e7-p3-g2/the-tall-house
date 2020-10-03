using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    class PlayerStunnedState : PlayerState
    {
        private IEnumerator blinkingCorutine;
        public PlayerStunnedState(PlayerController playerController) : base(playerController) { }

        public override void Enter()
        {
            PlayerController.Invoke("StopBeingStuned", PlayerController.StunnedTime);
            StartBlinking();
            PlayerController.OnStunned.Invoke();
            PlayerController.animationController.SetStunned();
            PlayerController.cameraController.ForceHideFrame();
        }

        private void StartBlinking()
        {
            blinkingCorutine = CreateBlinkingCorutine();
            PlayerController.StartCoroutine(blinkingCorutine);
        }

        private IEnumerator CreateBlinkingCorutine()
        {
            for (; ; )
            {
                yield return new WaitForSeconds(0.3f);
                Blink();
            }
        }

        private void Blink()
        {
            SpriteRenderer sprite = PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>();
            sprite.enabled = !sprite.enabled;
        }
        private void StopBlinking()
        {
            SpriteRenderer sprite = PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>();
            sprite.enabled = true;
            PlayerController.StopCoroutine(blinkingCorutine);
        }
        private IEnumerator WaitAndStopBeingStunned(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                StopBeingStuned();
            }
        }
        public override void StopBeingStuned()
        {
            PlayerController.SetState(new PlayerIdleState(PlayerController));
        }
        public override void Exit()
        {
            base.Exit();
            StopBlinking();
            PlayerController.PlayerAnimation.GetComponent<SpriteRenderer>().color = Color.white;
            PlayerController.WhatGhostsSee.SetActive(true);
            PlayerController.cameraController.ShowFrame();
        }
    }
}