using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecordableItem : MonoBehaviour
{
    public UnityEvent EnteredFrame;
    public UnityEvent ExitedFrame;
    public UnityEvent Recorded;
    [SerializeField] float RecordingRate = 1f / 30f; // once per frame for 30 FPS
    private bool IsBeingRecorded = false;
    private IEnumerator coroutine;
    private bool IsInFrame;
    public void StartRecording()
    {
        if (!IsInFrame)
        {
            return;
        }
        coroutine = WaitAndRecord();
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
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CameraFrame"))
        {
            IsInFrame = false;
            ExitedFrame.Invoke();
            StopRecording();
        }
    }
}
