using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace IStreamYouScream
{
    public class ActionOpenDoor : Action
    {
        public UnityEvent OnOpened;
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
                TextHint.SetText("Close door");
                DoorsOpen.SetActive(true);
                DoorsClosed.SetActive(false);
            }
            else
            {
                TextHint.SetText("Open door");
                DoorsOpen.SetActive(false);
                DoorsClosed.SetActive(true);
            }
        }

        public override void PerformAction()
        {
            IsOpen = !IsOpen;
            OnOpened.Invoke();
        }
    }
}