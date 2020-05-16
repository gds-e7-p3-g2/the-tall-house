using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraFrame : MonoBehaviour {
    [SerializeField] FlipController flipController;
    [SerializeField] GameObject cameraFrame;

    // Update is called once per frame
    void Update () {
        flipController.Face (transform.position.x > cameraFrame.transform.position.x, 1);
    }
}