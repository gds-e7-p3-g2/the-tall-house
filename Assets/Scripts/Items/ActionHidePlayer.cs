using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ActionHidePlayer : Action
    {
        private PlayerController playerController;
        [SerializeField] TextSetter TextHint;

        void Start()
        {
            TextHint.SetText("Press E to hide");
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public override void PerformAction()
        {
            playerController.ToggleHiding();

            if (playerController.GetIsHiding())
            {
                TextHint.SetText("Press E to stop hiding");
            }
            else
            {
                TextHint.SetText("Press E to hide");
            }

        }
    }
}