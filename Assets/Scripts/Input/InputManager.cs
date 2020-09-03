using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

namespace IStreamYouScream
{
    public class InputManager : MonoBehaviour
    {
        static public InputDevice ActiveDevice
        {
            get { return InControl.InputManager.ActiveDevice; }
        }

        static public Vector3 RightStickVector
        {
            get { return new Vector3(ActiveDevice.RightStickX, ActiveDevice.RightStickY, 0f); }
        }

        static public float Horizontal
        {
            get { return ActiveDevice.LeftStickX; }
        }

        static public bool Run
        {
            get { return ActiveDevice.LeftBumper != 0; }
        }

        static public bool Interact
        {
            get { return ActiveDevice.Action4.WasPressed; }
        }

        static public bool Recording
        {
            get { return ActiveDevice.RightBumper.IsPressed; }
        }

        static public bool Melee
        {
            get { return ActiveDevice.Action1.IsPressed; }
        }


    }
}