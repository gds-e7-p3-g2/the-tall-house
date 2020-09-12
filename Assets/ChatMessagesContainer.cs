using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IStreamYouScream
{
    public class ChatMessagesContainer : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] int MaxCharacters = 1000;

        public void AddMessage(string message)
        {
            string newText = (text.text + "\n" + message);
            if (newText.Length > MaxCharacters)
            {
                newText = newText.Substring(newText.Length - MaxCharacters);
            }
            text.text = newText;
        }

        #region singleton
        private static ChatMessagesContainer _instance;

        public static ChatMessagesContainer Instance { get { return _instance; } }

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