using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    class PlayerMovingState : PlayerState
    {
        protected Rigidbody2D rigidbody2D;
        private Vector3 VelocityZero = Vector3.zero;
        private float distanceMultiplier = 10f;
        protected float Speed = 0.0f;
        protected float horizontalMove = 0f;

        public PlayerMovingState(PlayerController playerController, float speed) : base(playerController)
        {
            Speed = speed;
        }

        public override void Move(float distance)
        {
            Vector3 targetVelocity = new Vector2(distance * distanceMultiplier * Speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
            rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref VelocityZero, 0.002f);
        }

        public override void Enter()
        {
            rigidbody2D = PlayerController.GetComponent<Rigidbody2D>();
        }
        public override void Exit()
        {
            rigidbody2D.velocity = VelocityZero;
        }
        public override void OnUpdate()
        {
            UpdateAnimation();
            horizontalMove = InputManager.Horizontal;
            if (horizontalMove == 0)
            {
                OnStopMoving();
            }
        }

        protected virtual void OnStopMoving()
        {
            PlayerController.SetState(new PlayerIdleState(PlayerController));
        }

        public override void OnFixedUpdate()
        {
            PlayerController.Move(horizontalMove);
        }

        protected virtual void UpdateAnimation()
        {
            // is the player going backwards
            if (PlayerController.IsCameraOnLeft() && horizontalMove > 0.0f || !PlayerController.IsCameraOnLeft() && horizontalMove < 0.0f)
            {
                PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", -1.0f);
            }
            else
            {
                PlayerController.PlayerAnimation.GetComponent<Animator>().SetFloat("Direction", 1.0f);
            }
        }

    }
}