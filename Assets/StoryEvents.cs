using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    [System.Serializable] public class ItemEvent : UnityEvent { };
    [System.Serializable] public class GhostEvent : UnityEvent<GhostController> { };
    [System.Serializable] public class PlayerEvent : UnityEvent { };

    [System.Serializable] public class StoryEvent : UnityEvent { };
    [System.Serializable] public class MiscEvent : UnityEvent { };
    public class StoryEvents : MonoBehaviour
    {
        #region eventy

        public ItemEvent OnFamilyPictureRecorded;
        public ItemEvent OnGrandfatherPictureRecorder;
        public GhostEvent OnGhostRecorded;
        public GhostEvent OnGhostStunned;
        public PlayerEvent OnPlayerStunned;
        public PlayerEvent OnMeleeUsed;
        public UnityEvent OnMusicboxFirstSpotted;
        public UnityEvent OnCoinInsertedToMusicbox;
        public UnityEvent OnMusicboxWrongChoice;
        public UnityEvent OnMusicboxWrongChoiceAgain;
        public StoryEvent OnGroundFloorPicturesRecorded;
        public StoryEvent OnMusicanBodyFound;
        public MiscEvent OnBoringPeriod;
        public MiscEvent OnRandom;
        public UnityEvent OnTick;

        #endregion


        // Start is called before the first frame update
        public void Tick()
        {
            OnTick.Invoke();
        }




        #region singleton
        private static StoryEvents _instance;

        public static StoryEvents Instance { get { return _instance; } }

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