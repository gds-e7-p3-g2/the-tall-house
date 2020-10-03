using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private float Attractivness = 5f;
        public List<string> Messages = new List<string>(){
            "Generic chat message"
        };
        public void OnRecorded()
        {
            StoryEvents.Instance.OnCollectableRecorded.Invoke(Attractivness);

            if (Messages.Count > 0)
            {
                StoryEvents.Instance.OnNamedCollectableRecorded.Invoke(Messages);
            }
        }
    }
}