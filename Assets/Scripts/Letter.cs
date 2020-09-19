using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    [System.Serializable]
    public class LetterLitEvent : UnityEvent<Letter> { }

    public class Letter : MonoBehaviour
    {
        public char letter;
        public float RecordingTime = 0.05f;
        public float Cooldown = 10f;
        public bool IsLitUp = false;
        private bool IsSolved = false;
        private float LeftToLitUp;
        private IEnumerator CooldownCorutine;

        public LetterLitEvent OnLitUp;
        public LetterLitEvent OnDeactivated;
        public LetterLitEvent OnSolved;

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
            OnLitUp.Invoke(this);
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
                OnDeactivated.Invoke(this);
            }
        }

        public void ForceDeactivate()
        {
            // Indicate that the painting was forcefull deactivated
            Deactivate(true);
        }

        public void MarkSolved()
        {
            IsSolved = true;
            OnSolved.Invoke(this);
        }

    }
}