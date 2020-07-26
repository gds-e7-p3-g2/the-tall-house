using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;

namespace IStreamYouScream
{
    [System.Serializable]
    public class OnPlayerSeen : UnityEvent<Vector3> { };
    public class ConeOfSight : MonoBehaviour
    {
        public OnPlayerSeen OnSeen;

        private void OnTriggerStay2D(Collider2D other)
        {
            OnSeen.Invoke(other.gameObject.transform.position);
        }
    }

}