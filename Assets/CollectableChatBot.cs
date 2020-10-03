using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class ListOfStrings : List<string> { };
    public class MessagesMap : Dictionary<string, List<string>> { }

    public class CollectableChatBot : MonoBehaviour
    {
        private Dictionary<string, Color> name2color = new Dictionary<string, Color>();
        public List<string> Usernames = new List<string>() {
            "nitpicker", "h1n732"
        };

        [SerializeField]
        private List<Color> Colors = new List<Color>() {
            Color.red, Color.blue, Color.cyan, Color.gray, Color.yellow
        };

        void Start()
        {
            StoryEvents.Instance.OnNamedCollectableRecorded.AddListener(React);
        }

        void React(List<string> messages)
        {
            SendRandomMessage(messages);
        }

        private string GetRandomUsername()
        {
            return Usernames[Random.Range(0, Usernames.Count)];
        }

        private Color GetUserColor(string username)
        {
            if (!name2color.ContainsKey(username))
            {
                name2color[username] = Colors[Random.Range(0, Colors.Count)];
            }
            return name2color[username];
        }

        private string GetRandomMessage(List<string> messages)
        {
            return messages[Random.Range(0, messages.Count)];
        }

        private void SendRandomMessage(List<string> messages)
        {

            string username = GetRandomUsername();
            Color color = GetUserColor(username);
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string message = GetRandomMessage(messages);

            ChatMessagesContainer.Instance.AddMessage($"<color=#{colorHex}>{username}:</color>: {message}");

        }
    }
}