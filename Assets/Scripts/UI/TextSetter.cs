using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace IStreamYouScream
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] Text Text;

        private Text GetText()
        {
            if (Text != null)
            {
                return Text;
            }
            return GetComponent<Text>();
        }

        public void SetText(string text)
        {
            GetText().text = text;
        }

        public void SetFloat(float number)
        {
            GetText().text = number.ToString();
        }

        public void SetInt(int number)
        {
            GetText().text = number.ToString();
        }
    }
}