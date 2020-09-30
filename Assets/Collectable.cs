using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private float Attractivness = 5f;
        public string Name = "Znajdźka";
        public void OnRecorded()
        {
            StoryEvents.Instance.OnCollectableRecorded.Invoke(Attractivness);

            if (Name.Length > 0)
            {
                StoryEvents.Instance.OnNamedCollectableRecorded.Invoke(Name);
            }
        }
    }
}