using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class ListOfStrings : List<string> { };
    public class MessagesMap : Dictionary<string, List<string>> { }

    public class CollectableChatBot : MonoBehaviour
    {
        public List<string> Usernames = new List<string>() {
            "nitpicker"
        };

        public MessagesMap messages = new MessagesMap() {
            {"Portret testowy", new List<string>(){ "Wiadomosc na testowa znajdzke 1", "Wiadomosc na testowa znajdzke 2"} },
            {"NAZWA ZNAJDZKI", new List<string>(){ "Msg 1", "Msg 2"} }
        };

        private List<Color> Colors = new List<Color>() {
            Color.red, Color.blue, Color.cyan, Color.gray, Color.yellow
        };

        void Start()
        {
            StoryEvents.Instance.OnNamedCollectableRecorded.AddListener(React);
        }

        void React(string collectableName)
        {
            SendRandomMessage(collectableName);
        }

        private void BindEvents()
        {
            // foreach (KeyValuePair<UnityEvent, Messages> entry in event2msg)
            // {
            //     entry.Key.AddListener(() => SendRandomMessage(entry.Key, entry.Value));
            // }
        }

        private string GetRandomUsername()
        {
            return Usernames[Random.Range(0, Usernames.Count)];
        }

        private Color GetUserColor(string username)
        {
            return Color.blue;
        }

        private string GetRandomMessage(string collectableName)
        {
            if (messages[collectableName] != null)
            {
                return messages[collectableName][Random.Range(0, messages[collectableName].Count)];
            }
            return "I see you Found " + collectableName;
        }

        private void SendRandomMessage(string collectableName)
        {

            string username = GetRandomUsername();
            Color color = GetUserColor(username);
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string message = GetRandomMessage(collectableName);

            ChatMessagesContainer.Instance.AddMessage($"<color=#{colorHex}>{username}:</color>: {message}");

        }
    }
}