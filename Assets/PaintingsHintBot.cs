using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class PaintingsHintBot : MonoBehaviour
    {
        public List<string> Usernames = new List<string>() {
            "m3liss4", "jm", "case_diya", "CH1M3R4", "L16H7N1N6", "Silversgleaming", "emOCoDe", "pacificfish", "pleaseamazeme", "icraveattention", "pinotnoir", "redrum20", "warrensfan1997", "sonicf2302", "mannieq", "lovehuskie", "lustforviews", "imjustbored", "bl33der", "late_boomer", "catatafish", "needkicks", "nothingmatters230913", "Patryk_N", "xAxAERS_o_O", "rEaDy10", "khanlord_2020", "idontsleep", "hikimori1986", "rosebud", "kimik0", "wenflon", "sridevi4ever", "saj28", "slither", "ashiscallingme", "kraft_punk", "ramadanSteve", "patricia_the_stripper", "cobra_kai20", "akamarU", "souchi", "tomie", "uzumaki_96", "stonerBoy2006", "edgy_teddy", "Peter_Paka", "PrinceAlbert", "itsamemario", "cococubana", "ladyjessica", "holdthedoor", "bruce_willis_was_a_ghost", "patatapon_nyc", "acdcfan13941", "m_myers", "hedonisticApe", "mothra_foka",
        };

        public List<string> Messages = new List<string>() {
            "there is a different number of people on each painting",
            "there is some order to those paintings",
            "hey record the paintings in some order and see whats gonna happen",
            "harv try to record paitings from that one room",
            "CHECK THE PAINTINGSSSSS",
            "Woooow dude maybe its a CODE! mind blown",
            "only one painting has ZERO people in it",
            "YO TRY TO RECORD THEM IN ORDER",
        };

        private string lastMsg;

        private List<Color> Colors = new List<Color>() {
            Color.red, Color.blue, Color.cyan, Color.gray, Color.yellow
        };

        private Dictionary<string, Color> name2color = new Dictionary<string, Color>();

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

        private string GetRandomMessage()
        {
            string msg = null;
            while (msg == null || msg == lastMsg)
            {
                msg = Messages[Random.Range(0, Messages.Count - 1)];
            }
            lastMsg = msg;
            return msg;
        }

        public void SendRandomMessage()
        {
            string username = GetRandomUsername();
            Color color = GetUserColor(username);
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string message = GetRandomMessage();

            ChatMessagesContainer.Instance.AddMessage("- - - - - - - - -");
            ChatMessagesContainer.Instance.AddMessage($"<color=#{colorHex}>{username}:</color>: {message}");
            ChatMessagesContainer.Instance.AddMessage("- - - - - - - - -");

            SFX.Play("Hint");
        }

    }
}