using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class StoryEvents : MonoBehaviour
    {
        #region eventy

        #region property_by_string_access
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
        #endregion

        public UnityEvent OnFamilyPictureRecorded;
        public UnityEvent OnGrandfatherPictureRecorder;
        public UnityEvent OnGhostRecorded;
        public UnityEvent OnGhostStunned;
        public UnityEvent OnPlayerStunned;
        public UnityEvent OnMeleeUsed;
        public UnityEvent OnMusicboxFirstSpotted;
        public UnityEvent OnCoinInsertedToMusicbox;
        public UnityEvent OnMusicboxWrongChoice;
        public UnityEvent OnMusicboxWrongChoiceAgain;
        public UnityEvent OnGroundFloorPicturesRecorded;
        public UnityEvent OnMusicanBodyFound;
        public UnityEvent OnBoringPeriod;
        public UnityEvent OnCoinPicked;
        public UnityEvent OnRandom;
        public UnityEvent OnTick;
        public UnityEvent OnLostAllViewers;
        public UnityEvent OnReachedEndOfTheGame;

        #endregion

        #region callers
        public void CallOnFamilyPictureRecorded0() { OnFamilyPictureRecorded.Invoke(); }
        public void CallOnGrandfatherPictureRecorder() { OnGrandfatherPictureRecorder.Invoke(); }
        public void CallOnGhostRecorded() { OnGhostRecorded.Invoke(); }
        public void CallOnGhostStunned() { OnGhostStunned.Invoke(); }
        public void CallOnPlayerStunned() { OnPlayerStunned.Invoke(); }
        public void CallOnMeleeUsed() { OnMeleeUsed.Invoke(); }
        public void CallOnMusicboxFirstSpotted() { OnMusicboxFirstSpotted.Invoke(); }
        public void CallOnCoinInsertedToMusicbox() { OnCoinInsertedToMusicbox.Invoke(); }
        public void CallOnMusicboxWrongChoice() { OnMusicboxWrongChoice.Invoke(); }
        public void CallOnMusicboxWrongChoiceAgain() { OnMusicboxWrongChoiceAgain.Invoke(); }
        public void CallOnGroundFloorPicturesRecorded() { OnGroundFloorPicturesRecorded.Invoke(); }
        public void CallOnMusicanBodyFound() { OnMusicanBodyFound.Invoke(); }
        public void CallOnBoringPeriod() { OnBoringPeriod.Invoke(); }
        public void CallOnRandom() { OnRandom.Invoke(); }
        public void CallOnCoinPicked() { OnCoinPicked.Invoke(); }
        public void CallOnTick() { OnTick.Invoke(); }

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