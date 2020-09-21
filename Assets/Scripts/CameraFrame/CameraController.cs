﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
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
        public virtual bool IsRecording() { return false; }
        public virtual void RegisterItem(RecordableItem item) { }
        public virtual void UnregisterItem(RecordableItem item) { }
    }

    public class CameraIdleState : CameraState
    {

        public CameraIdleState(CameraController cameraController) : base(cameraController) { }

        public override void Enter()
        {
            if (CameraController.BaterryLevel <= 0)
            {
                CameraController.SetState(new CameraNoEnergyState(CameraController));
            }
        }

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
            CameraController.BaterryLevel -= CameraController.PowerConsumption * Time.deltaTime;

            if (CameraController.BaterryLevel <= 0f)
            {
                StopRecording();
            }
        }
        public override bool IsRecording()
        {
            return true;
        }
        public override void HideFrame()
        {
            CameraController.SetState(new CameraHiddenState(CameraController));
        }
        public override void StopRecording()
        {
            CameraController.SetState(new CameraIdleState(CameraController));
        }
        private void ChangeColor(Color color)
        {
            CameraController.VisualRepresentation.GetComponent<Light2D>().color = color;
        }

        public override void Enter()
        {
            ChangeColor(Color.red);
            foreach (RecordableItem recordableItem in CameraController.RegisteredItems)
            {
                recordableItem.StartRecording();
            }
        }

        public override void RegisterItem(RecordableItem item)
        {
            item.StartRecording();
        }

        public override void Exit()
        {
            ChangeColor(Color.green);
            foreach (RecordableItem recordableItem in CameraController.RegisteredItems)
            {
                recordableItem.StopRecording();
            }
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

    public class CameraNoEnergyState : CameraState
    {
        public CameraNoEnergyState(CameraController cameraController) : base(cameraController) { }

        public override void Enter()
        {
            CameraController.VisualRepresentation.GetComponent<Light2D>().intensity = 0;
        }

        public override void Exit()
        {
            CameraController.VisualRepresentation.GetComponent<Light2D>().intensity = 1;
        }

        public override void StartCharging()
        {
            CameraController.SetState(new CameraChargingState(CameraController));
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

    public class CameraLoadingFlashState : CameraState
    {

        public CameraLoadingFlashState(CameraController cameraController) : base(cameraController) { }

        public override void Enter()
        {
            // if not enough energy - 
            //      Display Notification
            //       go to idle
            // start loading flash


            // in Update - if flash loading stoped - go to idle

            // in Invoke - go to FlashShotReady
        }
    }

    public class CameraController : StateMachine<CameraState>
    {
        public float _BaterryLevel = 100f;
        public GameObject LowBatteryIndicator;
        public float LowBatteryThreshold = 15f;
        public float FlashLoadingTime = 3f;
        public float FlashPowerConsumption = 25f;
        public List<RecordableItem> RegisteredItems = new List<RecordableItem>();

        public float BaterryLevel
        {
            get { return _BaterryLevel; }
            set
            {
                _BaterryLevel = Mathf.Min(Mathf.Max(value, 0f), 100f);
                OnBatteryLevelChanged.Invoke(BaterryLevel);
                LowBatteryIndicator.SetActive(BaterryLevel < LowBatteryThreshold);

                if (BaterryLevel < LowBatteryThreshold)
                {
                    MusicController.Instance.PlayLowBattery();
                }
            }
        }
        public float PowerConsumption = 1.5f;
        public float ChargingSpeed = 5.5f;
        public GameObject VisualRepresentation;
        public FloatEvent OnBatteryLevelChanged;

        void Start()
        {
            SetState(new CameraIdleState(this));
            BaterryLevel = _BaterryLevel;
        }

        // Update is called once per frame
        void Update()
        {
            CurrentState.OnUpdate();
            foreach (RecordableItem recordableItem in RegisteredItems)
            {
                Debug.DrawLine(transform.position, recordableItem.transform.position);
            }
        }

        public void RegisterItem(RecordableItem item)
        {
            RegisteredItems.Add(item);
            CurrentState.RegisterItem(item);
        }
        public void UnregisterItem(RecordableItem item)
        {
            RegisteredItems.Remove(item);
            CurrentState.UnregisterItem(item);
        }

        public void StartRecording() { CurrentState.StartRecording(); }
        public void StopRecording() { CurrentState.StopRecording(); }
        public void StartCharging() { CurrentState.StartCharging(); }
        public void StopCharging() { CurrentState.StopCharging(); }
        public void ShowFrame() { CurrentState.ShowFrame(); }
        public void HideFrame() { CurrentState.HideFrame(); }
        public bool IsRecording() { return CurrentState.IsRecording(); }
    }
}