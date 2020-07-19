using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class GhostState : State
    {
        protected GhostController GhostController;
        public GhostState(GhostController ghostController)
        {
            GhostController = ghostController;
        }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void TogglePatroling() { }
        public virtual void ToggleAlerted() { }
        public virtual void ToggleAttacking() { }
        public virtual void ToggleBeingStuned() { }
        public virtual void ToggleHiding() { }
        public virtual void ToggleDefeated() { }
        public virtual void CatchPlayer() { }
        public virtual void HitPlayer() { }
        public virtual void GetHitByFlash() { }
        public virtual void GetHitByEmergency() { }
        public virtual void GetHitByMelee() { }
    }

    class GhostPatrolingState : GhostState
    {
        public GhostPatrolingState(GhostController ghostController) : base(ghostController) { }
    }
    class GhostAlertedState : GhostState
    {
        public GhostAlertedState(GhostController ghostController) : base(ghostController) { }
    }
    class GhostAttackingState : GhostState
    {
        public GhostAttackingState(GhostController ghostController) : base(ghostController) { }
    }
    class GhostHidingState : GhostState
    {
        public GhostHidingState(GhostController ghostController) : base(ghostController) { }
    }
    class GhostDefeatedState : GhostState
    {
        public GhostDefeatedState(GhostController ghostController) : base(ghostController) { }
    }

    public class GhostController : StateMachine<GhostState>
    {
        public void Start()
        {
            SetState(new GhostPatrolingState(this));
        }

        void Update()
        {
            CurrentState.OnUpdate();
        }

        void FixedUpdate()
        {
            CurrentState.OnFixedUpdate();
        }

    }
}