using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public virtual void StartFighting(PlayerController player) { }
        public virtual void StopFighting() { }
        public virtual void StartBeingStuned()
        {
            GhostController.SetState(new GhosStunnedState(GhostController));
        }
        public virtual void StopBeingStuned() { }
        public virtual void StartHiding() { }
        public virtual void StopHiding() { }
        public virtual void StartBeingDefeated() { }
        public virtual void StopBeingDefeated() { }
        public virtual void HitPlayer() { }
        public virtual void GetHitByFlash() { }
        public virtual void GetHitByMelee() { }
        public virtual void GetRecorded() { }
        public virtual void GetFlashed() { }
        public virtual void OnTargetReached() { }
        public virtual void Heal()
        {
            GhostController.HP = Mathf.Min(100f, GhostController.HP + GhostController.HealingSpeed * Time.deltaTime);
        }
    }

    class GhostPatrolingState : GhostState
    {
        public GhostPatrolingState(GhostController ghostController) : base(ghostController) { }
        IEnumerator c;

        public override void Enter()
        {
            GhostController.Target = GhostController.PatrollingTargetPoint;
            GhostController.CurrentSpeed = GhostController.PatrolingSpeed;
            c = WaitAndPlayAmbient();
            GhostController.StartCoroutine(c);
        }

        public override void Exit()
        {
            if (c != null)
            {
                GhostController.StopCoroutine(c);
            }

        }

        IEnumerator WaitAndPlayAmbient()
        {
            yield return new WaitForSeconds(.5f);
            MusicController.Instance.PlayAmbient();
        }

        public override void StartAttacking()
        {
            GhostController.SetState(new GhostPreAttackingState(GhostController));
        }

        public override void StartAlerted()
        {
            GhostController.SetState(new GhostAlertedState(GhostController));
        }

        public override void GetRecorded()
        {
            GhostController.HP -= GhostController.DamageFromRecording;
            if (StoryEvents.Instance != null)
                StoryEvents.Instance.OnGhostRecorded.Invoke();
        }

        public override void OnUpdate()
        {
            if (GhostController.HP <= 0.05f)
            {
                GhostController.SetState(new GhostSusceptibleToFlashState(GhostController));
                return;
            }
            if (GhostController.HP < GhostController.AlertedHPThreshold)
            {
                StartAlerted();
                return;
            }
            Heal();
        }

        public override void GetHitByMelee()
        {
            StartBeingStuned();
        }
    }
    class GhostAlertedState : GhostState
    {
        public GhostAlertedState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.AlertedTargetPoint;
            GhostController.CurrentSpeed = GhostController.AlertedSpeed;
            MusicController.Instance.PlayAlerted();
        }
        public override void GetRecorded()
        {
            GhostController.HP = Mathf.Max(GhostController.HP - GhostController.DamageFromRecording, 0f);
            if (StoryEvents.Instance != null)
                StoryEvents.Instance.OnGhostRecorded.Invoke();
        }
        public override void StartAttacking()
        {
            GhostController.SetState(new GhostPreAttackingState(GhostController));
        }
        public override void StartPatroling()
        {
            GhostController.SetState(new GhostPatrolingState(GhostController));
        }
        public override void OnUpdate()
        {
            SFX.Play("GhostAlerted");

            if (GhostController.HP >= 99f)
            {
                StartPatroling();
                return;
            }
            if (GhostController.HP <= 0.05f)
            {
                GhostController.SetState(new GhostSusceptibleToFlashState(GhostController));
                return;
            }
            Heal();
        }
        public override void GetHitByMelee()
        {
            StartBeingStuned();
        }
    }

    class GhostPreAttackingState : GhostState
    {
        public GhostPreAttackingState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.LastSeenPoint;
            GhostController.CurrentSpeed = 0;

            SFX.Play("GhostAttackStart");

            GhostController.StartCoroutine(WaitAndAtack());
            GhostController.animationController.attacking = true;
        }

        IEnumerator WaitAndAtack()
        {
            yield return new WaitForSeconds(1);
            GhostController.SetState(new GhostAttackingState(GhostController));
        }
    }

    class GhostAttackingState : GhostState
    {
        public GhostAttackingState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.Target = GhostController.LastSeenPoint;
            GhostController.CurrentSpeed = GhostController.AttackingSpeed;
            MusicController.Instance.PlayAttacking();
        }

        public override void Exit()
        {
            GhostController.animationController.attacking = false;
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

        public override void StartFighting(PlayerController player)
        {
            GhostController.SetState(new GhostFightingState(GhostController, player));
        }

        public override void GetRecorded()
        {
            if (StoryEvents.Instance != null)
                StoryEvents.Instance.OnGhostRecorded.Invoke();
            GhostController.HP -= GhostController.DamageFromRecording;

            if (GhostController.HP <= 0.05f)
            {
                GhostController.SetState(new GhostSusceptibleToFlashState(GhostController));
                return;
            }
        }
        public override void GetHitByMelee()
        {
            StartBeingStuned();
        }
    }
    class GhostFightingState : GhostState
    {
        private PlayerController player;
        IEnumerator corutine;
        IEnumerator corutine2;
        bool waiting = false;
        public GhostFightingState(GhostController ghostController, PlayerController _player) : base(ghostController)
        {
            player = _player;
        }

        public override void Enter()
        {
            GhostController.Target = GameObject.FindWithTag("PlayerFront");
            MusicController.Instance.PlayFighting();
            GhostController.weapon.OnFire.AddListener(HitPlayer);
            GhostController.CurrentSpeed = GhostController.AttackingSpeed;
            GhostController.animationController.attacking = true;
        }

        public override void Exit()
        {
            GhostController.weapon.OnFire.RemoveListener(HitPlayer);

            if (corutine != null)
            {
                GhostController.StopCoroutine(corutine);
            }

            if (corutine2 != null)
            {
                GhostController.StopCoroutine(corutine2);
            }
        }

        public override void OnUpdate()
        {
            if (!waiting)
                GhostController.weapon.Shoot();
            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            GhostController.flipX = player.transform.position.x > GhostController.gameObject.transform.position.x;
        }

        public override void HitPlayer()
        {
            player.GetHitByGhost(GhostController.weapon.AmountOfDamage);

            corutine = Wait();
            GhostController.StartCoroutine(corutine);
        }

        IEnumerator Wait()
        {
            waiting = true;
            GhostController.CurrentSpeed = .1f;
            yield return new WaitForSeconds(3f);
            GhostController.CurrentSpeed = GhostController.AttackingSpeed;
            waiting = false;
            corutine = null;
        }

        IEnumerator WaitAndGetBackToAttacking()
        {
            waiting = true;
            GhostController.CurrentSpeed = .1f;
            yield return new WaitForSeconds(.5f);
            GhostController.SetState(new GhostPreAttackingState(GhostController));
            waiting = false;
            corutine2 = null;
        }

        public override void StopFighting()
        {
            corutine2 = WaitAndGetBackToAttacking();
            GhostController.StartCoroutine(corutine2);
        }

        public override void GetRecorded()
        {
            if (StoryEvents.Instance != null)
                StoryEvents.Instance.OnGhostRecorded.Invoke();
            GhostController.HP -= GhostController.DamageFromRecording;

            if (GhostController.HP <= 0.05f)
            {
                GhostController.SetState(new GhostSusceptibleToFlashState(GhostController));
                return;
            }
        }
        public override void GetHitByMelee()
        {
            StartBeingStuned();
        }
    }
    class GhosStunnedState : GhostState
    {
        private bool visible = true;
        public GhosStunnedState(GhostController ghostController) : base(ghostController) { }

        private IEnumerator blinkingCorutine;

        public override void Enter()
        {
            if (StoryEvents.Instance != null)
                StoryEvents.Instance.OnGhostStunned.Invoke();
            GhostController.CurrentSpeed = 0f;
            GhostController.Invoke("StopBeingStuned", GhostController.StunnedCooldown);
            StartBlinking();
        }

        private void StartBlinking()
        {
            blinkingCorutine = CreateBlinkingCorutine();
            GhostController.StartCoroutine(blinkingCorutine);
        }

        private IEnumerator CreateBlinkingCorutine()
        {
            for (; ; )
            {
                yield return new WaitForSeconds(0.3f);
                Blink();
            }
        }

        private void Blink()
        {
            visible = !visible;
            GhostController.VisualRepresentation.SetActive(visible);
        }
        private void StopBlinking()
        {
            GhostController.VisualRepresentation.SetActive(true);
            GhostController.StopCoroutine(blinkingCorutine);
        }
        private IEnumerator WaitAndStopBeingStunned(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                StopBeingStuned();
            }
        }
        public override void StopBeingStuned()
        {
            GhostController.SetState(new GhostPatrolingState(GhostController));
        }
        public override void Exit()
        {
            base.Exit();
            StopBlinking();
        }
    }
    class GhostHidingState : GhostState
    {
        public GhostHidingState(GhostController ghostController) : base(ghostController) { }
        private IEnumerator corutine;

        public override void Enter()
        {
            corutine = WaitAndComeBack();
            GhostController.StartCoroutine(corutine);

            // GhostController.GhostArea.gameObject.SetActive(false);
        }

        public override void Exit()
        {
            if (corutine != null)
            {
                GhostController.StopCoroutine(corutine);
            }
        }

        private IEnumerator WaitAndComeBack()
        {
            yield return new WaitForSeconds(15f);
            GhostController.HP = 100;
            GhostController.GhostArea.gameObject.SetActive(true);
            GhostController.SetState(new GhostPatrolingState(GhostController));
        }

    }

    class GhostSusceptibleToFlashState : GhostState
    {
        public GhostSusceptibleToFlashState(GhostController ghostController) : base(ghostController) { }
        private IEnumerator corutine;

        public override void Enter()
        {
            GhostController.Target = GhostController.PatrollingTargetPoint;
            GhostController.CurrentSpeed = 0.3f;
            MusicController.Instance.PlayAmbient();

            corutine = WaitAndGoToHideout();
            GhostController.StartCoroutine(corutine);

            GhostController.OnFlashable.Invoke();
            GhostController.ConeOfSight.SetActive(false);
        }

        public override void Exit()
        {
            if (corutine != null)
            {
                GhostController.StopCoroutine(corutine);
            }
        }

        private IEnumerator WaitAndGoToHideout()
        {
            yield return new WaitForSeconds(60f);
            GhostController.SetState(new GhostHidingState(GhostController));
        }

        public override void GetFlashed()
        {
            GhostController.SetState(new GhostDefeatedState(GhostController));
        }

    }

    class GhostDefeatedState : GhostState
    {
        private IEnumerator corutine;
        public GhostDefeatedState(GhostController ghostController) : base(ghostController) { }

        public override void Enter()
        {
            GhostController.CurrentSpeed = 0;

            // MusicController.Instance.PlayGhostDefeated();
            MusicController.Instance.PlayAmbient();

            GhostController.ConeOfSight.SetActive(false);

            GhostController.animationController.MarkDead();

            GhostController.StartCoroutine(WaitAndDie());

            SFX.Play("GhostDefeated");
        }

        private IEnumerator WaitAndDie()
        {
            yield return new WaitForSeconds(1.5f);
            GhostController.GhostArea.gameObject.SetActive(false);
        }
    }

    public class GhostController : StateMachine<GhostState>
    {
        private float prevX = 0;
        private bool isGoingLeft { get { return transform.position.x < prevX; } }
        public Weapon weapon;
        public GameObject VisualRepresentation;
        public GameObject LastSeenPoint;
        public GameObject AlertedTargetPoint;
        public GameObject PatrollingTargetPoint;
        public GameObject DefeatedPoint;
        public GameObject GhostArea;
        public GameObject ConeOfSight;
        public float PatrolingSpeed = 3f;
        public float AlertedSpeed = 6f;
        public float AttackingSpeed = 4.5f;
        public float DamageFromRecording = 0.3f;
        public float HealingSpeed = 0.5f;
        public float AlertedHPThreshold = 75f;
        public float FlashResistanceThreshold = 25f;
        public float StunnedCooldown = 5f;
        public UnityEvent OnDefeated;
        public UnityEvent OnFlashable;

        [SerializeField] TextSetter HPIndicator;
        private float _HP = 100f;
        private PlayerController player;
        public float HP
        {
            get { return _HP; }
            set
            {
                _HP = value;
                HPIndicator.SetText("HP " + HP.ToString("#.00") + "%");
            }
        }
        public bool flipX
        {
            get { return animationController.flipX; }
            set
            {
                if (flipX == value)
                {
                    return;
                }
                animationController.flipX = value;
            }
        }

        public GhostAnimationController animationController
        {
            get { return VisualRepresentation.GetComponent<GhostAnimationController>(); }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnPlayerInReach(other.gameObject.GetComponent<PlayerController>());
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            OnPlayerInReach(other.gameObject.GetComponent<PlayerController>());
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            OnPlayerOutOfReach();
        }

        public GameObject Target
        {
            get { return GetComponent<SmoothMoveToTarget>().Target; }
            set { GetComponent<SmoothMoveToTarget>().Target = value; }
        }
        public float CurrentSpeed
        {
            get { return GetComponent<SmoothMoveToTarget>().Speed; }
            set
            {
                if (Target.GetComponent<FollowPath>() != null)
                {
                    Target.GetComponent<FollowPath>().Speed = value;
                }
                GetComponent<SmoothMoveToTarget>().Speed = value;
            }
        }
        public void Start()
        {
            SetState(new GhostPatrolingState(this));
            HP = _HP;
        }

        public void Awake()
        {
            SetState(new GhostPatrolingState(this));
            HP = _HP;
        }

        private void OnPlayerInReach(PlayerController player)
        {
            this.player = player;
            StartFighting(player);
            player.InDanger = true;
        }
        private void OnPlayerOutOfReach()
        {
            StopFighting();
            player.InDanger = false;
        }

        private void FixFacingSide()
        {
            flipX = !isGoingLeft;
        }

        void Update()
        {
            if (CurrentState == null)
            {
                SetState(new GhostPatrolingState(this));
            }
            FixFacingSide();
            CurrentState.OnUpdate();

            prevX = transform.position.x;
        }
        void FixedUpdate() { CurrentState.OnFixedUpdate(); }
        public void OnTargetReached() { CurrentState.OnTargetReached(); }
        public void StartPatroling() { CurrentState.StartPatroling(); }
        public void StopPatroling() { CurrentState.StopPatroling(); }
        public void StartAlerted() { CurrentState.StartAlerted(); }
        public void StopAlerted() { CurrentState.StopAlerted(); }
        public void StartAttacking() { CurrentState.StartAttacking(); }
        public void StopAttacking() { CurrentState.StopAttacking(); }
        public void StartFighting(PlayerController player) { CurrentState.StartFighting(player); }
        public void StopFighting() { CurrentState.StopFighting(); }
        public void StartBeingStuned() { CurrentState.StartBeingStuned(); }
        public void StopBeingStuned() { CurrentState.StopBeingStuned(); }
        public void StartHiding() { CurrentState.StartHiding(); }
        public void StopHiding() { CurrentState.StopHiding(); }
        public void StartBeingDefeated() { CurrentState.StartBeingDefeated(); }
        public void StopBeingDefeated() { CurrentState.StopBeingDefeated(); }
        public void GetRecorded() { CurrentState.GetRecorded(); }
        public void GetFlashed() { CurrentState.GetFlashed(); }
        public void GetHitByMelee() { CurrentState.GetHitByMelee(); }
    }
}