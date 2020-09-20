using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

namespace IStreamYouScream
{
    public class PadMapping
    {
        public static InputControlType MELEE = InputControlType.Action2;
        public static InputControlType RECORD = InputControlType.RightBumper;
        public static InputControlType USE = InputControlType.Action1;
        public static InputControlType RUN = InputControlType.Action3;
        public static InputControlType FLASH = InputControlType.LeftTrigger;
    }

    // InputControlType.DPadUp
    // InputControlType.DPadDown
    // InputControlType.DPadLeft
    // InputControlType.DPadRight

    // InputControlType.Action1   -    X on PS4          |    A   on xbox
    // InputControlType.Action2   -    Circle on PS4     |    B   on xbox
    // InputControlType.Action3   -    Square on PS4     |    X   on xbox
    // InputControlType.Action4   -    Triangle on PS4   |    Y   on xbox

    // InputControlType.LeftTrigger
    // InputControlType.RightTrigger

    // InputControlType.LeftBumper
    // InputControlType.RightBumper

    // InputControlType.LeftStickButton
    // InputControlType.RightStickButton
}