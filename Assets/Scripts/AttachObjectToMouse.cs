using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObjectToMouse : MonoBehaviour {

    public Camera camera;
    public Vector3 mouseScreenPosition;
    public Vector3 mouseWorldPosition;

    void Update () {
        mouseScreenPosition = Input.mousePosition;
        mouseWorldPosition = camera.ScreenToWorldPoint (new Vector3 (mouseScreenPosition.x,
            mouseScreenPosition.y,
            camera.nearClipPlane + 1)); //The +1 is there so you don't overlap the object and the camera, otherwise the object is drawn "inside" of the camera, and therefore you're not able to see it!

        transform.position = mouseWorldPosition;
    }
}