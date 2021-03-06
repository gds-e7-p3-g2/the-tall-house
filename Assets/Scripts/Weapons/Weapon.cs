﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class Weapon : MonoBehaviour
    {
        public UnityEvent OnFire;
        public UnityEvent OnReady;
        public float CooldownInterval = 1f;
        public float AmountOfDamage = 1f;
        private bool isReadyToShoot = true;

        public void Shoot()
        {
            if (!isReadyToShoot)
            {
                return;
            }
            SendDamage();
            StartCoroutine(Reload());
        }

        private void SendDamage()
        {
            OnFire.Invoke();
        }

        private IEnumerator Reload()
        {
            isReadyToShoot = false;
            yield return new WaitForSeconds(CooldownInterval);
            isReadyToShoot = true;
            OnReady.Invoke();
        }
    }
}