using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace IStreamYouScream
{
    public class SoundsController : MonoBehaviour
    {
        public SoundEffect findSound(string name)
        {
            Transform child = transform.Find(name);
            Transform missing = transform.Find("Missing");
            child = child != null ? child : missing;

            SoundEffect s = child.GetComponent<SoundEffect>();

            return s != null ? s : missing.GetComponent<SoundEffect>();
        }

        public static void Play(string name)
        {
            Instance.findSound(name).Play();
        }

        #region singleton
        private static SoundsController _instance;

        public static SoundsController Instance { get { return _instance; } }

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

    public static class SFX
    {
        public static void Play(string name)
        {
            SoundsController.Play(name);
        }
    }
}