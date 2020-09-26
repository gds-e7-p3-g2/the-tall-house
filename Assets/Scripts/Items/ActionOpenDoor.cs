using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace IStreamYouScream
{
    public class ActionOpenDoor : Action
    {
        [SerializeField] private string OpenDoorText = "Open door";
        [SerializeField] private string CloseDoorText = "Close door";
        public UnityEvent OnOpened;
        public UnityEvent OnClosed;
        [SerializeField]
        private bool _IsOpen;
        bool IsOpen
        {
            get { return _IsOpen; }
            set
            {
                _IsOpen = value;
                UpdateView();
            }
        }
        [SerializeField] TextSetter TextHint;
        [SerializeField] GameObject DoorsOpen;
        [SerializeField] GameObject DoorsClosed;

        void Start()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (IsOpen)
            {
                TextHint.SetText(CloseDoorText);
                DoorsOpen.SetActive(true);
                DoorsClosed.SetActive(false);
            }
            else
            {
                TextHint.SetText(OpenDoorText);
                DoorsOpen.SetActive(false);
                DoorsClosed.SetActive(true);
            }
        }

        public override void PerformAction()
        {
            IsOpen = !IsOpen;
            if (IsOpen) {
                OnOpened.Invoke();
            } else {
                OnClosed.Invoke();
            }
        }
    }
}