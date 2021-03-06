﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

namespace IStreamYouScream
{
    public class InputManager : MonoBehaviour
    {
        #region Actions
        static public bool Run { get { return GetControl(PadMapping.RUN) != 0; } }
        static public bool Interact { get { return GetControl(PadMapping.USE).WasPressed; } }
        static public bool Recording { get { return GetControl(PadMapping.RECORD).IsPressed; } }
        static public bool FlashLoading { get { return GetControl(PadMapping.FLASH).WasPressed; } }
        static public bool FlashLoadingReleased { get { return GetControl(PadMapping.FLASH).WasReleased; } }
        static public bool Melee { get { return GetControl(PadMapping.MELEE).IsPressed; } }
        static public bool MenuAccept { get { return GetControl(PadMapping.MENU_ACCEPT).WasPressed; } }
        static public bool MenuDeny { get { return GetControl(PadMapping.MENU_DENY).WasPressed; } }

        static public bool Options
        {
            get
            {
                foreach (InputControlType i in PadMapping.ESCAPE)
                {
                    if (GetControl(i).WasPressed)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        #endregion

        #region Movement
        static public float Horizontal { get { return ActiveDevice.LeftStickX; } }
        static public Vector3 RightStickVector
        {
            get { return new Vector3(ActiveDevice.RightStickX, ActiveDevice.RightStickY, 0f); }
        }
        #endregion

        #region Technical Fields
        static public InputDevice ActiveDevice { get { return InControl.InputManager.ActiveDevice; } }
        static private InputControl GetControl(InputControlType type)
        {
            return ActiveDevice.GetControl(type);
        }
        #endregion
    }
}