using UnityEngine;
using UnityEngine.Events;
namespace IStreamYouScream
{
    public class ActionHidePlayer : Action
    {
        private PlayerController playerController;
        [SerializeField] TextSetter TextHint;
        public UnityEvent OnHidden;
        public UnityEvent OnVisible;

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
                OnHidden.Invoke();
            }
            else
            {
                TextHint.SetText("Press E to hide");
                OnVisible.Invoke();
            }

        }
    }
}