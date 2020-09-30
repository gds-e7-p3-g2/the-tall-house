using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace IStreamYouScream
{
    public class RecordableItem : MonoBehaviour
    {
        private CameraController CameraFrame;
        [SerializeField] float RecordingRate = 1f / 30f; // once per frame for 30 FPS
        [SerializeField] bool IsOneTime = false;

        private bool WasRecordedOneTime = false;
        public UnityEvent EnteredFrame;
        public UnityEvent ExitedFrame;
        public UnityEvent Recorded;
        public UnityEvent Flashed;
        private bool IsBeingRecorded = false;
        private IEnumerator coroutine;
        private bool IsInFrame;
        void Start()
        {
            CameraFrame = FindObjectOfType<CameraController>();
        }
        public void StartRecording()
        {

            Debug.Log("Recidring collectable");

            if (WasRecordedOneTime)
            {
                return;
            }
            if (!IsInFrame)
            {
                return;
            }
            if (IsOneTime)
            {
                Recorded.Invoke();
                WasRecordedOneTime = true;
            }
            else
            {
                coroutine = WaitAndRecord();
                StartCoroutine(coroutine);
            }
        }

        public void GetFlashed()
        {
            if (WasRecordedOneTime)
            {
                return;
            }
            if (!IsInFrame)
            {
                return;
            }
            Flashed.Invoke();
        }

        public void StopRecording()
        {
            if (WasRecordedOneTime)
            {
                return;
            }
            if (coroutine == null)
            {
                return;
            }
            StopCoroutine(coroutine);
        }

        private IEnumerator WaitAndRecord()
        {
            for (; ; )
            {
                Recorded.Invoke();
                yield return new WaitForSeconds(RecordingRate);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (WasRecordedOneTime)
            {
                return;
            }
            if (other.gameObject.CompareTag("CameraFrame"))
            {
                EnteredFrame.Invoke();
                IsInFrame = true;
                CameraFrame.RegisterRecordableItem(this);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (WasRecordedOneTime)
            {
                if (other.gameObject.CompareTag("CameraFrame"))
                {
                    CameraFrame.UnregisterRecordableItem(this);
                    IsInFrame = false;
                    StopRecording();
                }
                return;
            }
            if (other.gameObject.CompareTag("CameraFrame"))
            {
                CameraFrame.UnregisterRecordableItem(this);
                IsInFrame = false;
                ExitedFrame.Invoke();
                StopRecording();
            }
        }
    }
}