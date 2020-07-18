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
            TextHint.SetText("Charge battery");
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public override void PerformAction()
        {
            playerController.ToggleCharging();

            if (playerController.GetIsCharging())
            {
                TextHint.SetText("Stop charging");
            }
            else
            {
                TextHint.SetText("Charge battery");
            }

        }
    }
}