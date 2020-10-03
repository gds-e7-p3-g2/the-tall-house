using System.Collections;
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
        public virtual void FlashLoadingPressed() { }
        public virtual void FlashLoadingReleased() { }
        public virtual void StartCharging() { }
        public virtual void StopCharging() { }
        public virtual void ShowFrame() { }
        public virtual void HideFrame() { }
        public virtual void OnUpdate() { }
        public virtual bool IsRecording() { return false; }
        public virtual void RegisterRecordableItem(RecordableItem item) { }
        public virtual void UnregisterRecordableItem(RecordableItem item) { }
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

        public override void FlashLoadingPressed()
        {
            CameraController.SetState(new CameraFlashLoadingState(CameraController));
        }
    }

    public class CameraRecordingState : CameraState
    {
        public CameraRecordingState(CameraController cameraController) : base(cameraController) { }

        public override void OnUpdate()
        {
            SFX.Play("Recording");
            CameraController.BaterryLevel -= CameraController.PowerConsumption * Time.deltaTime;

            if (CameraController.BaterryLevel <= 0f)
            {
                StopRecording();
            }
        }
        public override void FlashLoadingPressed()
        {
            CameraController.SetState(new CameraFlashLoadingState(CameraController));
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
            CameraController.VisualRepresentation.GetComponent<Light2D>().enabled = true;
        }

        public override void Enter()
        {
            ChangeColor(Color.red);
            foreach (RecordableItem recordableItem in CameraController.RegisteredRecordableItems)
            {
                RegisterRecordableItem(recordableItem);
            }
        }

        public override void RegisterRecordableItem(RecordableItem item)
        {
            item.StartRecording();
        }

        public override void Exit()
        {
            ChangeColor(Color.green);
            foreach (RecordableItem recordableItem in CameraController.RegisteredRecordableItems)
            {
                recordableItem.StopRecording();
            }

            CameraController.VisualRepresentation.GetComponent<Light2D>().enabled = false;
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

    public class CameraFlashLoadingState : CameraState
    {

        public CameraFlashLoadingState(CameraController cameraController) : base(cameraController) { }

        IEnumerator coroutine;

        public override void Enter()
        {
            if (CameraController.BaterryLevel < CameraController.FlashPowerConsumption)
            {
                CameraController.FlashPowerTooLowIndicator.SetActive(true);
                CameraController.SetState(new CameraIdleState(CameraController));
                return;
            }

            // Go straigh to FIRE. We don't want to load the flash anymore;

            CameraController.SetState(new CameraFlashReadyState(CameraController));

            // coroutine = WaitAndGoToFlashShotReady();
            // CameraController.StartCoroutine(coroutine);
        }

        public override void Exit()
        {
            if (coroutine != null)
            {
                CameraController.StopCoroutine(coroutine);
            }
        }

        IEnumerator WaitAndGoToFlashShotReady()
        {
            yield return new WaitForSeconds(CameraController.FlashLoadingTime);

            CameraController.SetState(new CameraFlashReadyState(CameraController));
        }

        public override void FlashLoadingReleased()
        {
            CameraController.SetState(new CameraIdleState(CameraController));
        }
    }

    public class CameraFlashReadyState : CameraState
    {
        public CameraFlashReadyState(CameraController cameraController) : base(cameraController) { }

        public override void Enter()
        {
            CameraController.FlashReadyIndicator.SetActive(true);

            // FIRE at once, we don't want to wait for the trigger release anymore.

            FIRE();

            SoundsController.Instance.findSound("Flash").Play();
        }

        public override void Exit()
        {
            // CameraController.FlashReadyIndicator.SetActive(false);
        }

        // public override void FlashLoadingReleased()
        // {
        //     FIRE();
        // }

        private void FIRE()
        {
            // inform all registered flashable items that they were flashed.

            foreach (RecordableItem recordableItem in CameraController.RegisteredRecordableItems)
            {
                recordableItem.GetFlashed();
            }

            CameraController.BaterryLevel -= CameraController.FlashPowerConsumption;
            CameraController.SetState(new CameraIdleState(CameraController));
        }
    }

    public class CameraController : StateMachine<CameraState>
    {
        public float _BaterryLevel = 100f;
        public GameObject LowBatteryIndicator;
        public float LowBatteryThreshold = 15f;
        public float FlashLoadingTime = 3f;
        public float FlashPowerConsumption = 25f;
        public GameObject FlashPowerTooLowIndicator;
        public GameObject FlashReadyIndicator;
        public List<RecordableItem> RegisteredRecordableItems = new List<RecordableItem>();

        public float BaterryLevel
        {
            get { return _BaterryLevel; }
            set
            {
                float prev = BaterryLevel;
                _BaterryLevel = Mathf.Min(Mathf.Max(value, 0f), 100f);
                OnBatteryLevelChanged.Invoke(BaterryLevel);
                LowBatteryIndicator.SetActive(BaterryLevel < LowBatteryThreshold);

                if (BaterryLevel < LowBatteryThreshold)
                {
                    SoundsController.Instance.findSound("BatteryLow").Play();
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
            if (CurrentState == null)
            {
                SetState(new CameraIdleState(this));
            }
            CurrentState.OnUpdate();
            foreach (RecordableItem recordableItem in RegisteredRecordableItems)
            {
                Debug.DrawLine(transform.position, recordableItem.transform.position);
            }
        }

        public void RegisterRecordableItem(RecordableItem item)
        {
            RegisteredRecordableItems.Add(item);
            CurrentState.RegisterRecordableItem(item);
        }
        public void UnregisterRecordableItem(RecordableItem item)
        {
            RegisteredRecordableItems.Remove(item);
            CurrentState.UnregisterRecordableItem(item);
        }

        public void StartRecording() { CurrentState.StartRecording(); }
        public void StopRecording() { CurrentState.StopRecording(); }
        public void FlashLoadingPressed() { CurrentState.FlashLoadingPressed(); }
        public void FlashLoadingReleased() { CurrentState.FlashLoadingReleased(); }
        public void StartCharging() { CurrentState.StartCharging(); }
        public void StopCharging() { CurrentState.StopCharging(); }
        public void ShowFrame() { CurrentState.ShowFrame(); }
        public void HideFrame() { CurrentState.HideFrame(); }

        public void ForceHideFrame()
        {
            SetState(new CameraHiddenState(this));
        }
        public bool IsRecording() { return CurrentState.IsRecording(); }
    }
}