using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ActionChargeBattery : Action
    {
        private PlayerController playerController;
        [SerializeField] TextSetter TextHint;

        void Start()
        {
            TextHint.SetText("Press E to charge battery and record the Ghost");
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public override void PerformAction()
        {
            playerController.ToggleCharging();

            if (playerController.GetIsCharging())
            {
                TextHint.SetText("E to stop charging");
                MusicController.Instance.PlayPlugIn();
            }
            else
            {
                TextHint.SetText("Press E to charge battery and record the ghost");
                MusicController.Instance.PlayPlugOut();
            }

        }
    }
}