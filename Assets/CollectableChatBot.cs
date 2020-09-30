using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class CollectableChatBot : MonoBehaviour
    {
        private List<string> Usernames = new List<string>() {
            "nitpicker"
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
            // if (!name2color.ContainsKey(username))
            // {
            //     name2color[username] = Colors[Random.Range(0, Colors.Count)];
            // }
            // return name2color[username];
        }

        private void SendRandomMessage(string collectableName)
        {

            string username = GetRandomUsername();
            Color color = GetUserColor(username);
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string message = "I see you Found " + collectableName;

            ChatMessagesContainer.Instance.AddMessage($"<color=#{colorHex}>{username}:</color>: {message}");

        }

        // private IEnumerator Cooldown(UnityEvent e)
        // {
        //     event2cool[e] = true;

        //     yield return new WaitForSeconds(timeToCooldown);

        //     event2cool[e] = false;
        // }
    }
}