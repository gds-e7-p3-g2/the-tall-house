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
            Debug.Log("Ghost in melee range");
            player.MeleeWeapon.OnFire.AddListener(ReceiveHit);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Ghost OUT OF  melee range");
            player.MeleeWeapon.OnFire.RemoveListener(ReceiveHit);
        }

        private void ReceiveHit()
        {
            Debug.Log("ReceiveHit");
            OnHit.Invoke();
        }

    }
}