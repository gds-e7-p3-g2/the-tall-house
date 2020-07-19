﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
namespace IStreamYouScream
{
    public class CameraState : State
    {
        protected CameraController CameraController;
        public CameraState(CameraController cameraController)
        {
            CameraController = cameraController;
        }
        public virtual void StartRecording() { }
        public virtual void StopRecording() { }
        public virtual void StartCharging() { }
        public virtual void StopCharging() { }
        public virtual void ShowFrame() { }
        public virtual void HideFrame() { }
        public virtual void OnUpdate() { }
    }

    public class CameraIdleState : CameraState
    {
        public CameraIdleState(CameraController cameraController) : base(cameraController) { }

        public override void StartRecording()
        {
            CameraController.SetState(new CameraRecordingState(CameraController));
        }

        public override void StartCharging()
        {
            CameraController.SetState(new CameraChargingState(CameraController));
        }

        public override void HideFrame()
        {
            CameraController.SetState(new CameraHiddenState(CameraController));
        }
    }

    public class CameraRecordingState : CameraState
    {
        public CameraRecordingState(CameraController cameraController) : base(cameraController) { }

        public override void OnUpdate()
        {
            CameraController.BaterryLevel = Mathf.Max(CameraController.BaterryLevel - CameraController.PowerConsumption * Time.deltaTime, 0f);
            if (CameraController.BaterryLevel <= 0f)
            {
                StopRecording();
            }
        }
        public override void HideFrame()
        {
            CameraController.SetState(new CameraHiddenState(CameraController));
        }
        public override void StopRecording()
        {
            CameraController.SetState(new CameraIdleState(CameraController));
        }

        public override void Enter()
        {
            CameraController.VisualRepresentation.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = Color.red;
        }

        public override void Exit()
        {
            CameraController.VisualRepresentation.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = Color.green;
        }
    }
    public class CameraChargingState : CameraState
    {
        public CameraChargingState(CameraController cameraController) : base(cameraController) { }
        public override void OnUpdate()
        {
            CameraController.BaterryLevel = Mathf.Min(CameraController.BaterryLevel + CameraController.ChargingSpeed * Time.deltaTime, 100f);
        }
        public override void StopCharging()
        {
            CameraController.SetState(new CameraIdleState(CameraController));
        }
    }

    public class CameraHiddenState : CameraState
    {
        public CameraHiddenState(CameraController cameraController) : base(cameraController) { }

        public override void ShowFrame()
        {
            CameraController.SetState(new CameraIdleState(CameraController));
        }

        public override void Enter()
        {
            CameraController.VisualRepresentation.SetActive(false);
        }

        public override void Exit()
        {
            CameraController.VisualRepresentation.SetActive(true);
        }
    }

    public class CameraController : StateMachine<CameraState>
    {
        private float _BaterryLevel = 100f;
        public float BaterryLevel
        {
            get { return _BaterryLevel; }
            set
            {
                _BaterryLevel = value;
                BatteryLevelIndicator.SetText(_BaterryLevel + "%");
            }
        }
        public float PowerConsumption = 1.5f;
        public float ChargingSpeed = 5.5f;
        public GameObject VisualRepresentation;
        [SerializeField] TextSetter BatteryLevelIndicator;

        void Start()
        {
            SetState(new CameraIdleState(this));
        }

        // Update is called once per frame
        void Update() { CurrentState.OnUpdate(); }

        public void StartRecording() { CurrentState.StartRecording(); }
        public void StopRecording() { CurrentState.StopRecording(); }
        public void StartCharging() { CurrentState.StartCharging(); }
        public void StopCharging() { CurrentState.StopCharging(); }
        public void ShowFrame() { CurrentState.ShowFrame(); }
        public void HideFrame() { CurrentState.HideFrame(); }
    }
}