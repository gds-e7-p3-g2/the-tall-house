using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class GhostHitReceiver : MonoBehaviour
    {
        public UnityEvent OnHit;
        private PlayerController player;

        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            player.MeleeWeapon.OnFire.AddListener(ReceiveHit);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            player.MeleeWeapon.OnFire.RemoveListener(ReceiveHit);
        }

        private void ReceiveHit()
        {
            OnHit.Invoke();
        }

    }
}