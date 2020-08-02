﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] AudioSource AmbientMusic;
        [SerializeField] AudioSource AlertedMusic;
        [SerializeField] AudioSource AttackingMusic;
        private AudioSource CurrentSource;
        private void PlaySource(AudioSource source)
        {
            AudioSource NewSource = source.GetComponent<AudioSource>();
            if (CurrentSource == NewSource)
            {
                return;
            }
            if (CurrentSource != null)
            {
                CurrentSource.Stop();
            }
            CurrentSource = source.GetComponent<AudioSource>();
            CurrentSource.Play();
        }

        public void PlayAmbient()
        {
            PlaySource(AmbientMusic);
        }
        public void PlayAlerted()
        {
            PlaySource(AlertedMusic);

        }
        public void PlayAttacking()
        {
            PlaySource(AttackingMusic);
        }
    }
}