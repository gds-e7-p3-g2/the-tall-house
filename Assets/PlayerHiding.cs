using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private bool IsHiding = false;
    [SerializeField] GameObject cameraFrame;

    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnterHideout()
    {
        Debug.Log("Enter hideout");
        cameraFrame.SetActive(false);
        mySpriteRenderer.color = Color.black;
        IsHiding = true;
    }

    public void LeaveHideout()
    {
        Debug.Log("Leave hideout");
        cameraFrame.SetActive(false);
        mySpriteRenderer.color = Color.white;
        IsHiding = false;
        cameraFrame.SetActive(true);
    }

    public bool GetIsHiding()
    {
        return IsHiding;
    }
}