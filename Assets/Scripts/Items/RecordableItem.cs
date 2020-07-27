using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace IStreamYouScream
{
    public class RecordableItem : MonoBehaviour
    {
        private CameraController CameraFrame;
        public UnityEvent EnteredFrame;
        public UnityEvent ExitedFrame;
        public UnityEvent Recorded;
        [SerializeField] float RecordingRate = 1f / 30f; // once per frame for 30 FPS
        private bool IsBeingRecorded = false;
        private IEnumerator coroutine;
        private bool IsInFrame;
        void Start()
        {
            CameraFrame = FindObjectOfType<CameraController>();
        }
        public void StartRecording()
        {
            if (!IsInFrame)
            {
                return;
            }
            coroutine = WaitAndRecord();
            StartCoroutine(coroutine);
        }

        public void StopRecording()
        {
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
                yield return new WaitForSeconds(RecordingRate);
                Recorded.Invoke();
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("CameraFrame"))
            {
                EnteredFrame.Invoke();
                IsInFrame = true;
                CameraFrame.RegisteredItems.Add(this);

            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("CameraFrame"))
            {
                CameraFrame.RegisteredItems.Remove(this);
                IsInFrame = false;
                ExitedFrame.Invoke();
                StopRecording();
            }
        }
    }
}