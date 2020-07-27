using System.Collections;
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
            if (CurrentSource != null)
            {
                Debug.Log("Stopping " + CurrentSource.clip.name);
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