using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IStreamYouScream
{
    public class PauseScreen : MonoBehaviour
    {
        public GameObject panel;
        private bool isPAused = false;


        // Update is called once per frame
        void Update()
        {
            if (InputManager.Options)
            {
                toggle();
            }
        }

        public void toggle()
        {
            if (isPAused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }

        public void pause()
        {
            panel.SetActive(true);
            Time.timeScale = 0;
            isPAused = true;
        }

        public void resume()
        {
            Time.timeScale = 1f;
            isPAused = false;
            panel.SetActive(false);
        }

        public void unfrezze()
        {
            Time.timeScale = 1f;
        }
    }
}