using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class FamilyPainting : MonoBehaviour
    {
        private bool IsLitUp = false;
        private bool IsSolved = false;
        public int NumberOfPeople;
        public float RecordingTime = 0.5f;
        private float LeftToLitUp;
        public float Cooldown = 5f;
        private IEnumerator CooldownCorutine;

        public UnityEvent OnLitUp;
        public UnityEvent OnDeactivated;

        void Start()
        {
            LeftToLitUp = RecordingTime;
        }

        public void StartRecording()
        {
            if (IsLitUp || IsSolved)
            {
                return;
            }
            LeftToLitUp -= Time.deltaTime * Time.timeScale;
            if (LeftToLitUp <= 0)
            {
                LitUp();
            }
        }

        public void ResetTimeToLitUp()
        {
            LeftToLitUp = RecordingTime;
        }

        private void LitUp()
        {
            IsLitUp = true;
            StartCooldown();
            OnLitUp.Invoke();
        }

        private void StartCooldown()
        {
            if (IsSolved)
            {
                return;
            }
            CooldownCorutine = CreateCooldownCorutine();
            StartCoroutine(CooldownCorutine);
        }

        private IEnumerator CreateCooldownCorutine()
        {
            yield return new WaitForSeconds(Cooldown);
            Deactivate();
        }

        private void Deactivate(bool invokeEvent = true)
        {
            if (IsSolved)
            {
                return;
            }
            if (CooldownCorutine != null)
            {
                StopCoroutine(CooldownCorutine);
            }
            IsLitUp = false;
            ResetTimeToLitUp();
            if (invokeEvent)
            {
                OnDeactivated.Invoke();
            }
        }

        public void ForceDeactivate()
        {
            // Indicate that the painting was forcefull deactivated
            Deactivate();
        }

        public void MarkSolved()
        {
            IsSolved = true;
        }

    }
}
