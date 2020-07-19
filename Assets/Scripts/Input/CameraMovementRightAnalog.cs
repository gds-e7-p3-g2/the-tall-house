using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class CameraMovementRightAnalog : MonoBehaviour
    {
        public Camera camera;
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

            var bottomLeft = camera.ScreenToWorldPoint(Vector3.zero);
            var topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight));

            var cameraRect = new Rect(
                bottomLeft.x + 2f,
                bottomLeft.y + 2f,
                topRight.x - bottomLeft.x - 4f,
                topRight.y - bottomLeft.y - 4f);

            newPosition = new Vector3(
               Mathf.Clamp(newPosition.x, cameraRect.xMin, cameraRect.xMax),
               Mathf.Clamp(newPosition.y, cameraRect.yMin, cameraRect.yMax),
               newPosition.z
           );

            transform.position = newPosition;
        }
    }
}