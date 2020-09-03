using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;

namespace IStreamYouScream
{

    public class ConeOfSight : MonoBehaviour
    {
        public Vector3Event OnSeen;
        private void OnTriggerStay2D(Collider2D other)
        {
            OnSeen.Invoke(other.gameObject.transform.position);
        }
    }

}