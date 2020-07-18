using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
namespace IStreamYouScream
{
    public class AddKeyboardMapping : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            InControl.InputManager.AttachDevice(new UnityInputDevice(new KeyboardAndMouseProfile()));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}