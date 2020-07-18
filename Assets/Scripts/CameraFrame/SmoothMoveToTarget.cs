using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class SmoothMoveToTarget : MonoBehaviour
    {
        [SerializeField] GameObject Target;
        [SerializeField] float Speed = 100.0f;

        void Update()
        {
            Vector3 newPos = Vector3.Lerp(transform.position, Target.transform.position, Time.deltaTime * Speed);
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }
}