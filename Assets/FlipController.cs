using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipController : MonoBehaviour {
    private SpriteRenderer mySpriteRenderer;
    private int currPriority = 0;

    private void Awake () {
        mySpriteRenderer = GetComponent<SpriteRenderer> ();
    }

    void FaceLeft () {
        mySpriteRenderer.flipX = true;
    }

    void FaceRight () {
        mySpriteRenderer.flipX = false;
    }

    public void Face (bool left, int priority) {
        if (priority < currPriority) {
            return;
        }
        currPriority = priority;
        mySpriteRenderer.flipX = left;
    }

    public void Clear () {
        currPriority = 0;
    }
}