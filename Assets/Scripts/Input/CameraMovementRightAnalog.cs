using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class CameraMovementRightAnalog : MonoBehaviour
    {
        public GameObject LeftBottomLimit;
        public GameObject RightTopLimit;
        private Vector3 startPos;
        private Transform thisTransform;
        public float sensitivity = 0.2f;
        void Start()
        {
            thisTransform = transform;
            startPos = thisTransform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 inputDirection = InputManager.RightStickVector;

            Vector3 newPosition = transform.position + inputDirection * sensitivity;

            var cameraRect = new Rect(
                LeftBottomLimit.transform.position.x,
                LeftBottomLimit.transform.position.y,
                RightTopLimit.transform.position.x - LeftBottomLimit.transform.position.x,
                RightTopLimit.transform.position.y - LeftBottomLimit.transform.position.y);

            newPosition = new Vector3(
               Mathf.Clamp(newPosition.x, cameraRect.xMin, cameraRect.xMax),
               Mathf.Clamp(newPosition.y, cameraRect.yMin, cameraRect.yMax),
               newPosition.z
           );

            transform.position = newPosition;
        }
    }
}