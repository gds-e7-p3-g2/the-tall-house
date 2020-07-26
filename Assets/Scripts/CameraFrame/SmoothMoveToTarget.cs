using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class SmoothMoveToTarget : MonoBehaviour
    {
        public GameObject Target;
        public float Speed = 100.0f;
        public UnityEvent OnTargetReached;

        void Update()
        {
            Vector3 newPos = Vector3.Lerp(transform.position, Target.transform.position, Time.deltaTime * Speed);
            newPos.z = transform.position.z;

            Debug.Log(Vector3.Distance(newPos, transform.position));

            if (Vector3.Distance(newPos, transform.position) < 0.01f)
            {
                OnTargetReached.Invoke();
            }

            transform.position = newPos;

            Debug.DrawLine(transform.position, Target.transform.position, Color.white);
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, .5f);
        }
    }
}