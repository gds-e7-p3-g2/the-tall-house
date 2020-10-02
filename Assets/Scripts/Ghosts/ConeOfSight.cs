using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;





namespace IStreamYouScream
{
    public class CoSState : State
    {
        protected ConeOfSight _c { get { return this.controller; } }
        protected ConeOfSight controller;
        public CoSState(ConeOfSight controller) { this.controller = controller; }
        public virtual void StartLoading() { }
        public virtual void StartUnloading() { }
        public virtual void OnPlayerInSight() { }
        public virtual void OnPlayerOutOfSight() { }
        public virtual void Update() { }
    }

    public class CoSIdleState : CoSState
    {
        public CoSIdleState(ConeOfSight c) : base(c) { }
        public override void Enter()
        {
            _c.DestColor = _c.NormalColor;
        }
        public override void OnPlayerInSight()
        {
            _c.SetState(new CoSLoadingState(_c));
        }
    }

    public class CoSLoadingState : CoSState
    {
        public CoSLoadingState(ConeOfSight c) : base(c) { }
        IEnumerator corutine;
        public override void Enter()
        {
            _c.DestColor = _c.DangerLoadingColor;
            corutine = WaitAndGoToLoaded();
            _c.StartCoroutine(corutine);
        }

        public override void Exit()
        {
            if (corutine != null)
            {
                _c.StopCoroutine(corutine);
            }

        }

        private IEnumerator WaitAndGoToLoaded()
        {
            yield return new WaitForSeconds(_c.LoadingTime);
            _c.SetState(new CoSPlayerSpottedState(_c));
        }

        public override void OnPlayerOutOfSight()
        {
            _c.SetState(new CoSIdleState(_c));
        }
    }

    class CoSPlayerSpottedState : CoSState
    {
        public CoSPlayerSpottedState(ConeOfSight c) : base(c) { }
        public override void Enter()
        {
            _c.OnSpotted.Invoke();
            _c.SetState(new CoSIdleState(_c));
        }
    }
    public class ConeOfSight : StateMachine<CoSState>
    {
        public Color NormalColor = Color.white;
        public Color DangerLoadingColor = Color.red;
        public Vector3Event OnSeen;
        public UnityEvent OnSpotted;
        public Vector3 LastKnownPosition;
        public float LoadingTime = 2f;
        public Light2D coneLight { get { return gameObject.GetComponent<Light2D>(); } }
        private Color _DestColor;
        [HideInInspector] public float colorTimeCurr;
        public Color DestColor
        {
            set
            {
                _DestColor = value;
                colorTimeCurr = 0;
            }
        }

        void Start()
        {
            SetState(new CoSIdleState(this));
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            OnSeen.Invoke(other.gameObject.transform.position);
            CurrentState.OnPlayerInSight();
        }

        private void OnTriggerExit2D(Collider2D other)
        {

            CurrentState.OnPlayerOutOfSight();
        }

        private void Update()
        {
            if (CurrentState == null)
            {
                SetState(new CoSIdleState(this));
            }
            CurrentState.Update();
            ColorUpdate();
        }

        public void ColorUpdate()
        {
            if (colorTimeCurr > LoadingTime || coneLight.color == _DestColor)
            {
                return;
            }
            colorTimeCurr += Time.deltaTime * Time.timeScale;

            coneLight.color = Color.Lerp(coneLight.color, _DestColor, colorTimeCurr / LoadingTime);
        }
    }

}