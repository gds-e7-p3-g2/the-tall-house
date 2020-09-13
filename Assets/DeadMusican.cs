using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class DeadMusican : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            StoryEvents.Instance.OnMusicanBodyFound.Invoke();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
