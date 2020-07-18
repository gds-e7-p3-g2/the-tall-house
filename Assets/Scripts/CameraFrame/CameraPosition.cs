using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class CameraPosition : MonoBehaviour
    {
        [SerializeField] GameObject Target;
        [SerializeField] float Speed = 100.0f;

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, Time.deltaTime * Speed);
        }
    }
}