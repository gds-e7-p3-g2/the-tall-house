using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ActionEmpty : Action
    {
        private PlayerController playerController;
        [SerializeField] TextSetter TextHint;
        private int TimesUsed = 0;

        void Start()
        {
            TextHint.SetText("Action performed times: " + TimesUsed);
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public override void PerformAction()
        {
            TimesUsed++;
            TextHint.SetText("Action performed times: " + TimesUsed);
        }
    }
}