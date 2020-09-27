using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateCameraAnchor : MonoBehaviour
{
    public GameObject LeftBottom;
    public GameObject RightTop;
    public GameObject Middle;

    void Start()
    {
        FixPosition();
    }

    void FixedUpdate()
    {
        FixPosition();
    }

    private void FixPosition()
    {
        Vector3 CameraBottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 CameraTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        float xMiddle = Middle.transform.position.x;
        float xLeft = LeftBottom.transform.position.x;
        float xRight = RightTop.transform.position.x;
        float xCamLeft = CameraBottomLeft.x;
        float xCamRight = CameraTopRight.x;

        float yMiddle = Middle.transform.position.y;
        float yBottom = LeftBottom.transform.position.y;
        float yTop = RightTop.transform.position.y;
        float yCamBottom = CameraBottomLeft.y;
        float yCamTop = CameraTopRight.y;

        float xRatio = (xMiddle - xLeft) / (xRight - xLeft);
        float yRatio = (yMiddle - yBottom) / (yTop - yBottom);

        float xCamMiddle = (xCamRight - xCamLeft) * xRatio + xCamLeft;
        float yCamMiddle = (yCamTop - yCamBottom) * yRatio + yCamBottom;

        transform.position = new Vector3(xCamMiddle, yCamMiddle, 0);
    }
}
