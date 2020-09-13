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
        [SerializeField] AudioClip GhostDeath;
        [SerializeField] AudioClip PickCoin;
        [SerializeField] AudioClip CoinUsed;
        [SerializeField] AudioClip PlugIn;
        [SerializeField] AudioClip PlugOut;
        [SerializeField] AudioClip PowerDrain;
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

        private void PlayEffect(AudioClip clip)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
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

        public void PlayGhostDefeated()
        {
            PlayEffect(GhostDeath);
        }

        public void PlayPickedCoin()
        {
            PlayEffect(PickCoin);
        }

        public void PlayUseCoin()
        {
            PlayEffect(CoinUsed);
        }

        public void PlayPlugIn()
        {
            PlayEffect(PlugIn);
        }

        public void PlayPlugOut()
        {
            PlayEffect(PlugOut);
        }

        public void PlayLowBattery()
        {
            PlayEffect(PowerDrain);
        }

        #region singleton
        private static MusicController _instance;

        public static MusicController Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void OnDestroy() { if (this == _instance) { _instance = null; } }
        #endregion
    }
}