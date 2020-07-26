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
        public virtual void StartPatroling() { }
        public virtual void StopPatroling() { }
        public virtual void StartAlerted() { }
        public virtual void StopAlerted() { }
        public virtual void StartAttacking() { }
        public virtual void StopAttacking() { }
        public virtual void StartBeingStuned() { }
        public virtual void StopBeingStuned() { }
        public virtual void StartHiding() { }
        public virtual void StopHiding() { }
        public virtual void StartBeingDefeated() { }
        public virtual void StopBeingDefeated() { }
        public virtual void CatchPlayer() { }
        public virtual void HitPlayer() { }
        public virtual void GetHitByFlash() { }
        public virtual void GetHitByEmergency() { }
        public virtual void GetHitByMelee() { }
        public virtual void OnTargetReached() { }
    }

    class GhostPatrolingState : GhostState
    {
        public GhostPatrolingState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.PatrollingTargetPoint;
        }

        public override void StartAttacking()
        {
            GhostController.SetState(new GhostAttackingState(GhostController));
        }
    }
    class GhostAlertedState : GhostState
    {
        public GhostAlertedState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.AlertedTargetPoint;
        }
    }
    class GhostAttackingState : GhostState
    {
        public GhostAttackingState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.LastSeenPoint;
        }

        public override void OnTargetReached()
        {
            if (GhostController.Target == GhostController.LastSeenPoint)
            {
                StopAttacking();
            }
        }

        public override void StopAttacking()
        {
            GhostController.SetState(new GhostPatrolingState(GhostController));
        }
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
        public GameObject LastSeenPoint;
        public GameObject AlertedTargetPoint;
        public GameObject PatrollingTargetPoint;
        public GameObject Target
        {
            get { return GetComponent<SmoothMoveToTarget>().Target; }
            set { GetComponent<SmoothMoveToTarget>().Target = value; }
        }
        public float Speed
        {
            get { return GetComponent<SmoothMoveToTarget>().Speed; }
            set { GetComponent<SmoothMoveToTarget>().Speed = value; }
        }
        public void Start()
        {
            SetState(new GhostPatrolingState(this));
        }

        public GameObject GetCurrentTarget()
        {
            return GetComponent<SmoothMoveToTarget>().Target;
        }

        void Update() { CurrentState.OnUpdate(); }
        void FixedUpdate() { CurrentState.OnFixedUpdate(); }
        public void OnTargetReached() { CurrentState.OnTargetReached(); }
        public void StartPatroling() { CurrentState.StartPatroling(); }
        public void StopPatroling() { CurrentState.StopPatroling(); }
        public void StartAlerted() { CurrentState.StartAlerted(); }
        public void StopAlerted() { CurrentState.StopAlerted(); }
        public void StartAttacking() { CurrentState.StartAttacking(); }
        public void StopAttacking() { CurrentState.StopAttacking(); }
        public void StartBeingStuned() { CurrentState.StartBeingStuned(); }
        public void StopBeingStuned() { CurrentState.StopBeingStuned(); }
        public void StartHiding() { CurrentState.StartHiding(); }
        public void StopHiding() { CurrentState.StopHiding(); }
        public void StartBeingDefeated() { CurrentState.StartBeingDefeated(); }
        public void StopBeingDefeated() { CurrentState.StopBeingDefeated(); }

    }
}