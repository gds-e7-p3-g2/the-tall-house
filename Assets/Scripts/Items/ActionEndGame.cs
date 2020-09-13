using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IStreamYouScream
{
    public class ActionEndGame : Action
    {
        public override void PerformAction()
        {
            StoryEvents.Instance.OnReachedEndOfTheGame.Invoke();
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}