using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class Tick : MonoBehaviour
    {
        [SerializeField] float interval = 1f;
        [SerializeField] UnityEvent OnTick;

        private IEnumerator corutine;
        void Start()
        {
            Schedule();
        }

        public void Schedule()
        {
            StartCoroutine(Ticker());
        }

        private IEnumerator Ticker()
        {
            yield return new WaitForSeconds(interval);
            OnTick.Invoke();
        }
    }
}