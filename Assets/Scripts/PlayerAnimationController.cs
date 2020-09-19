﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator { get { return gameObject.GetComponent<Animator>(); } }
    private bool _flipX = false;

    public bool flipX
    {
        get { return _flipX; }
        set
        {
            if (flipX == value)
            {
                return;
            }
            _flipX = value;

            updateFacingSide();
        }
    }

    private void updateFacingSide()
    {
        Quaternion.LookRotation(new Vector3(0, 180, 0));
        if (flipX)
            animator.transform.rotation = Quaternion.LookRotation(Vector3.back);
        else
            animator.transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    enum STATES : int
    {
        IDLE = 0,
        WALK = 1,
        RUN = 2,
        MELEE = 3
    }

    private Dictionary<int, float> StateToSpeed = new Dictionary<int, float>(){
        { (int)STATES.IDLE, 0f },
        { (int)STATES.WALK, 1f },
        { (int)STATES.RUN, 1f },
        { (int)STATES.MELEE, 1f }
    };

    private void SetState(STATES state)
    {
        animator.SetInteger("State", (int)state);
        SetSpeedMultiplied(StateToSpeed[(int)state]);
    }
    private void SetSpeedMultiplied(float value)
    {
        animator.SetFloat("Direction", value);
    }
    public void SetIdle() { SetState(STATES.IDLE); }
    public void SetCharging() { SetState(STATES.IDLE); }
    public void SetWalk() { SetState(STATES.WALK); }
    public void SetRun() { SetState(STATES.RUN); }
    public void SetMelee() { SetState(STATES.MELEE); }
}