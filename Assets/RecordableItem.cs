using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecordableItem : MonoBehaviour
{
    public UnityEvent EnteredFrame;
    public UnityEvent ExitedFrame;
    public UnityEvent Recorded;
    public void BeRecorder()
    {
        if (IsInFrame)
        {
            Recorded.Invoke();
        }
    }

    private bool IsInFrame = false;

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
            ExitedFrame.Invoke();
            IsInFrame = false;
        }
    }
}
