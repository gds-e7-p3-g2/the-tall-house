using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public override void OnUpdate()
        {

        }
    }

    public class CameraRecordingState : CameraState
    {
        public CameraRecordingState(CameraController cameraController) : base(cameraController) { }
    }


    public class CameraChargingState : CameraState
    {
        public CameraChargingState(CameraController cameraController) : base(cameraController) { }
    }

    public class CameraHiddenState : CameraState
    {
        public CameraHiddenState(CameraController cameraController) : base(cameraController) { }
    }

    public class CameraController : StateMachine<CameraState>
    {
        public float BaterryLevel = 100.0f;
        public float PowerConsumption = 1.5f;

        void Start()
        {
            SetState(new CameraIdleState(this));
        }

        // Update is called once per frame
        void Update()
        {
            CurrentState.OnUpdate();
        }
    }
}