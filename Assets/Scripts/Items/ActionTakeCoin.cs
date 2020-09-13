using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ActionTakeCoin : Action
    {
        private PlayerController playerController;
        [SerializeField] TextSetter TextHint;
        [SerializeField] GameObject CoinVisuals;
        private bool isItemAvaiable = true;
        [SerializeField] bool IsDEBUG = false;

        void Start()
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public override void PerformAction()
        {
            if (IsDEBUG)
            {
                playerController.setHasCoin(true);
                return;
            }
            if (!isItemAvaiable)
            {
                return;
            }
            playerController.setHasCoin(true);
            isItemAvaiable = false;
            gameObject.SetActive(false);
        }
    }
}