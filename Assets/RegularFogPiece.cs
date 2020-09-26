using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class RegularFogPiece : MonoBehaviour
    {
        public bool IsActive = false;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Unfog();
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            Unfog();
        }

        private void Unfog()
        {
            if (!IsActive) return;
            StartCoroutine(FadeImage());
        }
        private void onEnd()
        {
            Destroy(gameObject);
        }

        IEnumerator FadeImage()
        {
            while (gameObject.GetComponent<SpriteRenderer>().color.a > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, Time.deltaTime / 3);
                yield return null;
            }
            onEnd();
        }
    }
}