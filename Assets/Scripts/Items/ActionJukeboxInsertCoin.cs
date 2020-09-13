using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ActionJukeboxInsertCoin : Action
    {
        private PlayerController playerController;
        [SerializeField] TextSetter TextHint;
        [SerializeField] GameObject Prize;
        [SerializeField] GameObject OnLights;
        private bool IsSolved;

        private string GetText()
        {
            if (IsSolved)
            {
                return "";
            }
            if (playerController.HasCoin())
            {
                return "Insert coin";
            }
            return "Insert coin\n<size=50>(coin needed)</size>";
        }

        void Start()
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

            UpdateText();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            UpdateText();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            TextHint.SetText(GetText());
        }

        public override void PerformAction()
        {
            if (!playerController.HasCoin())
            {
                return;
            }
            playerController.setHasCoin(false);
            IsSolved = true;
            StoryEvents.Instance.OnCoinInsertedToMusicbox.Invoke();
            Prize.SetActive(true);
            OnLights.SetActive(true);
            UpdateText();
        }
    }
}