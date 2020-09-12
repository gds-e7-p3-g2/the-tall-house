using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    class Messages : List<string> { };
    class EventToMessages : Dictionary<UnityEvent, Messages> { };
    class EventToCooldown : Dictionary<UnityEvent, bool> { };

    public class CrowdChatBot : MonoBehaviour
    {
        private EventToMessages event2msg;
        private EventToCooldown event2cool = new EventToCooldown();
        [SerializeField] private float timeToCooldown = 5f;
        [SerializeField]
        private List<string> Usernames = new List<string>() {
            "m3liss4", "jm", "case_diya", "CH1M3R4", "L16H7N1N6", "Silversgleaming"
        };

        private List<Color> Colors = new List<Color>() {
            Color.red, Color.blue, Color.cyan, Color.gray, Color.yellow
        };

        private Dictionary<string, Color> name2color = new Dictionary<string, Color>();

        void Start()
        {
            event2msg = new EventToMessages() {
                {
                    StoryEvents.Instance.OnFamilyPictureRecorded,
                    new Messages() {
                        "creepy!",
                        "the father look kinda familiar idk"
                    }
                },
                {
                    StoryEvents.Instance.OnGhostRecorded,
                    new Messages() {
                        "holy shit a real ghost :O",
                        "this is going viral",
                        "cgi",
                        "OMG",
                        "im not sleeping tonight",
                        "so wait ghosts are real",
                        "cleverandsceptical: whatever its all fake",
                        "holy f**k! an actual ghost",
                        "show us more",
                        "its getting weaker?",
                        "the camera weakens it? somehow?",
                        "power of seth :D",
                        "thats one tense ghost"
                    }
                },
                {
                    StoryEvents.Instance.OnMeleeUsed,
                    new Messages() {
                        "Don't swing this phone",
                        "Can't see sith",
                    }
                }
            };
            BindEvents();
        }

        private void BindEvents()
        {
            foreach (KeyValuePair<UnityEvent, Messages> entry in event2msg)
            {
                entry.Key.AddListener(() => SendRandomMessage(entry.Key, entry.Value));
            }
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

        private void SendRandomMessage(UnityEvent e, Messages m)
        {
            if (event2cool.ContainsKey(e) && event2cool[e])
            {
                return;
            }

            string username = GetRandomUsername();
            Color color = GetUserColor(username);
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string message = m[Random.Range(0, m.Count)];

            ChatMessagesContainer.Instance.AddMessage($"<color=#{colorHex}>{username}:</color>: {message}");

            StartCoroutine(Cooldown(e));
        }

        private IEnumerator Cooldown(UnityEvent e)
        {
            Debug.Log("COOLDOWN");
            Debug.Log(e);
            event2cool[e] = true;

            yield return new WaitForSeconds(timeToCooldown);

            event2cool[e] = false;
        }
    }
}