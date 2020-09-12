using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ChatBot : MonoBehaviour
    {
        public Color UserColor;
        public string UserName;

        public List<string> Messages;
        public void SendRandomMessage()
        {
            string color = ColorUtility.ToHtmlStringRGB(UserColor);
            string message = Messages[Random.Range(0, Messages.Count)];
            ChatMessagesContainer.Instance.AddMessage($"<color=#{color}>{UserName}:</color>: {message}");
        }
    }
}
