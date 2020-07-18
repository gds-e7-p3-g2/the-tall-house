using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace IStreamYouScream
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] Text Text;

        public void SetText(string text)
        {
            Text.text = text;
        }
    }
}