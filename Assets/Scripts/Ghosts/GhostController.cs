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
        public virtual void GetHitByMelee() { }
        public virtual void GetRecorded() { }
        public virtual void OnTargetReached() { }
        public virtual void Heal()
        {
            GhostController.HP = Mathf.Min(100f, GhostController.HP + GhostController.HealingSpeed * Time.deltaTime);
        }
    }

    class GhostPatrolingState : GhostState
    {
        public GhostPatrolingState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.PatrollingTargetPoint;
            GhostController.CurrentSpeed = GhostController.PatrolingSpeed;
        }

        public override void StartAttacking()
        {
            GhostController.SetState(new GhostAttackingState(GhostController));
        }

        public override void StartAlerted()
        {
            GhostController.SetState(new GhostAlertedState(GhostController));
        }

        public override void GetRecorded()
        {
            GhostController.HP -= GhostController.DamageFromRecording;
        }

        public override void OnUpdate()
        {
            if (GhostController.HP < GhostController.AlertedHPThreshold)
            {
                StartAlerted();
                return;
            }
            Heal();
        }
    }
    class GhostAlertedState : GhostState
    {
        public GhostAlertedState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.AlertedTargetPoint;
            GhostController.CurrentSpeed = GhostController.AlertedSpeed;
        }

        public override void GetRecorded()
        {
            GhostController.HP = Mathf.Max(GhostController.HP - GhostController.DamageFromRecording, 0f);
        }
        public override void StartAttacking()
        {
            GhostController.SetState(new GhostAttackingState(GhostController));
        }
        public override void StartPatroling()
        {
            GhostController.SetState(new GhostPatrolingState(GhostController));
        }
        public override void OnUpdate()
        {
            if (GhostController.HP >= GhostController.AlertedHPThreshold)
            {
                StartPatroling();
                return;
            }
            Heal();
        }
    }
    class GhostAttackingState : GhostState
    {
        public GhostAttackingState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.LastSeenPoint;
            GhostController.CurrentSpeed = GhostController.AttackingSpeed;
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

        public override void GetRecorded()
        {
            GhostController.HP -= GhostController.DamageFromRecording;
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
        public float PatrolingSpeed = 3f;
        public float AlertedSpeed = 6f;
        public float AttackingSpeed = 4.5f;
        public float DamageFromRecording = 0.3f;
        public float HealingSpeed = 0.5f;
        public float AlertedHPThreshold = 75f;
        public float FlashResistanceThreshold = 25f;
        [SerializeField] TextSetter HPIndicator;
        private float _HP = 100f;
        public float HP
        {
            get { return _HP; }
            set
            {
                _HP = value;
                HPIndicator.SetText("HP " + HP.ToString("#.00") + "%");
            }
        }
        public GameObject Target
        {
            get { return GetComponent<SmoothMoveToTarget>().Target; }
            set { GetComponent<SmoothMoveToTarget>().Target = value; }
        }
        public float CurrentSpeed
        {
            get { return GetComponent<SmoothMoveToTarget>().Speed; }
            set { GetComponent<SmoothMoveToTarget>().Speed = value; }
        }
        public void Start()
        {
            SetState(new GhostPatrolingState(this));
            HP = _HP;
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
        public void GetRecorded() { CurrentState.GetRecorded(); }
    }
}