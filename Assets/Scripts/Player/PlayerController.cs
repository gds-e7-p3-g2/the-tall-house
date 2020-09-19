using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class PlayerController : StateMachine<PlayerState>
    {
        private bool _hasCoin = false;
        public CameraController cameraController;
        public GameObject WhatGhostsSee;
        public float walkSpeed = 40f;
        public float runSpeed = 80f;
        public Weapon MeleeWeapon;
        public float StunnedTime = 2.5f;
        public GameObject PlayerAnimation;
        public UnityEvent OnStunned;

        private float meleeInitialX;

        public bool HasCoin()
        {
            return _hasCoin;
        }
        public void setHasCoin(bool v)
        {
            _hasCoin = v;
            if (_hasCoin)
            {
                StoryEvents.Instance.OnCoinPicked.Invoke();
            }
        }

        public bool flipX
        {
            get { return PlayerAnimation.GetComponent<SpriteRenderer>().flipX; }
            set
            {
                if (PlayerAnimation.GetComponent<SpriteRenderer>().flipX == value)
                {
                    return;
                }
                PlayerAnimation.GetComponent<SpriteRenderer>().flipX = value;

                var tmp = MeleeWeapon.transform.position;
                tmp.x += value ? -1.37f * 2f : 1.37f * 2f;
                MeleeWeapon.transform.position = tmp;

                tmp = MeleeWeapon.transform.localScale;
                tmp.x = value ? -1 : 1;
                MeleeWeapon.transform.localScale = tmp;
            }
        }

        public PlayerAnimationController animationController
        {
            get { return PlayerAnimation.GetComponent<PlayerAnimationController>(); }
        }

        public void Start()
        {
            SetState(new PlayerIdleState(this));
            // meleeInitialX = MeleeWeapon.transform.position.x;
        }
        public void Move(float distance)
        {
            CurrentState.Move(distance);
        }

        void Update()
        {
            CurrentState.OnUpdate();

            if (InputManager.Melee)
            {
                PerformMelee();
            }
        }

        void FixedUpdate()
        {
            CurrentState.OnFixedUpdate();
        }

        public void ToggleHiding()
        {
            CurrentState.ToggleHiding();
        }

        public void ToggleCharging()
        {
            CurrentState.ToggleCharging();
        }

        public bool IsCameraOnLeft()
        {
            return transform.position.x > cameraController.transform.position.x;
        }

        public void LookAtCamera()
        {
            FlipX(IsCameraOnLeft());
        }

        public void FlipX(bool flipX)
        {
            CurrentState.FlipX(flipX);
        }

        public bool GetIsHiding()
        {
            return CurrentState.GetIsHiding();
        }

        public bool GetIsCharging()
        {
            return CurrentState.GetIsCharging();
        }

        public void GetHitByGhost(float AmountOfDamage)
        {
            CurrentState.GetHitByGhost(AmountOfDamage);
        }

        public void StopBeingStuned()
        {
            CurrentState.StopBeingStuned();
        }

        public void PerformMelee()
        {
            CurrentState.PerformMelee();
        }

    }
}