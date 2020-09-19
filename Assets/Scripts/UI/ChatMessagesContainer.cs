using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace IStreamYouScream
{
    public class ChatMessagesContainer : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] int MaxMessages = 20;

        private List<string> messages = new List<string>();

        public void AddMessage(string message)
        {
            messages.Add(message);
            while (messages.Count > MaxMessages)
            {
                messages.RemoveAt(0);
            }
            UpdateText();
        }

        private void UpdateText()
        {
            text.text = messages.Aggregate((i, j) => i + "\n" + j);
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