﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

namespace IStreamYouScream
{
    public class PadMapping
    {
        public static InputControlType MELEE = InputControlType.Action1;
        public static InputControlType RECORD = InputControlType.RightBumper;
        public static InputControlType USE = InputControlType.Action4;
        public static InputControlType RUN = InputControlType.LeftBumper;
        public static InputControlType FLASH = InputControlType.RightTrigger;
    }

    // InputControlType.DPadUp
    // InputControlType.DPadDown
    // InputControlType.DPadLeft
    // InputControlType.DPadRight

    // InputControlType.Action1   -    X on PS4
    // InputControlType.Action2   -    Circle on PS4
    // InputControlType.Action3   -    Square on PS4
    // InputControlType.Action4   -    Triangle on PS4

    // InputControlType.LeftTrigger
    // InputControlType.RightTrigger

    // InputControlType.LeftBumper
    // InputControlType.RightBumper

    // InputControlType.LeftStickButton
    // InputControlType.RightStickButton
}