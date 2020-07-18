using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class PlayerState : State
    {
        protected PlayerController PlayerController;
        public PlayerState(PlayerController playerController)
        {
            PlayerController = playerController;
        }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }

        public virtual void Move(float distance) { }
        public virtual void Walk() { }
        public virtual void Run() { }
        public virtual void PickItem() { }
        public virtual void UseItem() { }
        public virtual void ToggleHiding() { }
        public virtual void StartRecording() { }
        public virtual void StopRecording() { }
        public virtual void StartBeingInDanger() { }
        public virtual void StopBeingInDanger() { }
        public virtual void PerformFlashAttack() { }
        public virtual void PerformEmergencyFlashAttack() { }
        public virtual void StartBeingStuned() { }
        public virtual void StopBeingStuned() { }
        public virtual void Lose() { }
        public virtual bool GetIsHiding() { return false; }
        public virtual void FlipX(bool flipX) { }

    }
}