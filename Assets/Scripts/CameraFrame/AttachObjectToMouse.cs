using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class AttachObjectToMouse : MonoBehaviour
    {
        public Camera camera;
        private Vector3 mouseScreenPosition;
        private Vector3 mouseWorldPosition;

        void Update()
        {
            mouseScreenPosition = Input.mousePosition;
            mouseWorldPosition = camera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x,
                mouseScreenPosition.y,
                camera.nearClipPlane + 1)); //The +1 is there so you don't overlap the object and the camera, otherwise the object is drawn "inside" of the camera, and therefore you're not able to see it!

            var bottomLeft = camera.ScreenToWorldPoint(Vector3.zero);
            var topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight));

            var cameraRect = new Rect(
                bottomLeft.x + 2f,
                bottomLeft.y + 2f,
                topRight.x - bottomLeft.x - 4f,
                topRight.y - bottomLeft.y - 4f);

            Vector3 dest = new Vector3(
                Mathf.Clamp(mouseWorldPosition.x, cameraRect.xMin, cameraRect.xMax),
                Mathf.Clamp(mouseWorldPosition.y, cameraRect.yMin, cameraRect.yMax),
                transform.position.z
            );

            Vector3 newPos = Vector3.Lerp(transform.position, dest, Time.deltaTime * 100);
            transform.position = newPos;
        }
    }
}