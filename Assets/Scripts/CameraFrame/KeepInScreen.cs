using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInScreen : MonoBehaviour
{
    [SerializeField] Camera camera;
    void Update()
    {
        var bottomLeft = camera.ScreenToWorldPoint(Vector3.zero);
        var topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight));

        var cameraRect = new Rect(
            bottomLeft.x + 2f,
            bottomLeft.y + 2f,
            topRight.x - bottomLeft.x - 4f,
            topRight.y - bottomLeft.y - 4f);

        Vector3 dest = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
            Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax),
            transform.position.z
        );

        Vector3 newPos = Vector3.Lerp(transform.position, dest, Time.deltaTime * 100);
        transform.position = newPos;
    }
}
