﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class PlayerController : StateMachine<PlayerState>
    {
        public CameraController cameraController;
        public GameObject WhatGhostsSee;
        public float walkSpeed = 40f;
        public float runSpeed = 80f;
        public GameObject PlayerAnimation;

        public void Start()
        {
            SetState(new PlayerIdleState(this));
        }
        public void Move(float distance)
        {
            CurrentState.Move(distance);
        }

        void Update()
        {
            CurrentState.OnUpdate();
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

    }
}