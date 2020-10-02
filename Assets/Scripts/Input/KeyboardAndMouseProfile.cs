using System;
using System.Collections;
using UnityEngine;
using InControl;


namespace IStreamYouScream
{
    // This custom profile is enabled by adding it to the Custom Profiles list
    // on the InControlManager component, or you can attach it yourself like so:
    // InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
    // 
    public class KeyboardAndMouseProfile : UnityInputDeviceProfile
    {
        public KeyboardAndMouseProfile()
        {
            #region tech fields
            Name = "Keyboard/Mouse";
            Meta = "A keyboard and mouse combination profile appropriate for FPS.";

            SupportedPlatforms = new[]
            {
                "Windows",
                "Mac",
                "Linux"
            };

            Sensitivity = 1.0f;
            LowerDeadZone = 0.0f;
            UpperDeadZone = 1.0f;
            #endregion

            ButtonMappings = new[]
            {
                new InputControlMapping
                {
                    Handle = "Melee",
                    Target = PadMapping.MELEE,
                    Source = MouseButton0  // Left mouse button
                },
                new InputControlMapping
                {
                    Handle = "Use",
                    Target = PadMapping.USE,
                    Source = KeyCodeButton( KeyCode.E )
                },
                new InputControlMapping
                {
                    Handle = "Record",
                    Target = PadMapping.RECORD,
                    Source = MouseButton1 // right mouse button
                },
                new InputControlMapping
                {
                    Handle = "Run",
                    Target = PadMapping.RUN,
                    Source = KeyCodeComboButton( KeyCode.LeftShift )
                },
                new InputControlMapping
                {
                    Handle = "Flash",
                    Target = PadMapping.FLASH,
                    Source = KeyCodeComboButton( KeyCode.Space )
                },
                new InputControlMapping
                {
                    Handle = "ESCAPE",
                    Target = PadMapping.ESCAPE[0],
                    Source = KeyCodeComboButton( KeyCode.Escape )
                },
                new InputControlMapping
                {
                    Handle = "Pause",
                    Target = PadMapping.ESCAPE[0],
                    Source = KeyCodeComboButton( KeyCode.P )
                },
            };

            #region AnalogMappings
            AnalogMappings = new[]
            {
                new InputControlMapping
                {
                    Handle = "Move X",
                    Target = InputControlType.LeftStickX,
                    Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
                },
                new InputControlMapping
                {
                    Handle = "Move Y",
                    Target = InputControlType.LeftStickY,
                    Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
                },
                new InputControlMapping {
                    Handle = "Move X Alternate",
                    Target = InputControlType.LeftStickX,
                    Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
                },
                new InputControlMapping {
                    Handle = "Move Y Alternate",
                    Target = InputControlType.LeftStickY,
                    Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
                },
                new InputControlMapping
                {
                    Handle = "Look X",
                    Target = InputControlType.RightStickX,
                    Source = MouseXAxis,
                    Raw    = true,
                    Scale  = 1f
                },
                new InputControlMapping
                {
                    Handle = "Look Y",
                    Target = InputControlType.RightStickY,
                    Source = MouseYAxis,
                    Raw    = true,
                    Scale  = 1f
                },
                new InputControlMapping
                {
                    Handle = "Look Z",
                    Target = InputControlType.ScrollWheel,
                    Source = MouseScrollWheel,
                    Raw    = true,
                    Scale  = 1f
                }
            };
            #endregion
        }
    }
}

