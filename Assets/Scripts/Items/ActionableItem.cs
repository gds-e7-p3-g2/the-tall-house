using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class ActionableItem : MonoBehaviour
    {
        private bool CanPerformAction = false;
        public ActionHint actionHint;
        public Action actionPerformer;
        public UnityEvent OnPerformed;

        public void ShowActionHint()
        {
            actionHint.Show();
            CanPerformAction = true;
        }

        public void HideActionHint()
        {
            actionHint.Hide();
            CanPerformAction = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ShowActionHint();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            HideActionHint();
        }

        // Start is called before the first frame update
        void Start()
        {
            actionPerformer = GetComponent<Action>();
        }

        void Update()
        {
            if (InputManager.Interact && CanPerformAction)
            {
                actionPerformer.PerformAction();
                OnPerformed.Invoke();
            }
        }
    }
}