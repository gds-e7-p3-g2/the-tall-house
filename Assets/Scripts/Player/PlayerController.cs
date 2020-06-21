using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine<PlayerState>
{
    [SerializeField] GameObject cameraFrame;
    public float walkSpeed = 40f;
    public float runSpeed = 80f;
    public GameObject PlayerAnimation;

    public void SetCameraFrameActive(bool cameraFrameActive)
    {
        cameraFrame.SetActive(cameraFrameActive);
    }

    public void Start()
    {
        SetState(new PlayerIdleState(this));
    }
    public void Move(float distance)
    {
        CurrentState.Move(distance);
    }

    void Update()
    {
        CurrentState.OnUpdate();
    }

    void FixedUpdate()
    {
        CurrentState.OnFixedUpdate();
    }

    public void ToggleHiding()
    {
        CurrentState.ToggleHiding();
    }

    public bool IsCameraOnLeft()
    {
        return transform.position.x > cameraFrame.transform.position.x;
    }

    public void LookAtCamera()
    {
        FlipX(IsCameraOnLeft());
    }

    public void FlipX(bool flipX)
    {
        CurrentState.FlipX(flipX);
    }

    public bool GetIsHiding()
    {
        return CurrentState.GetIsHiding();
    }

}