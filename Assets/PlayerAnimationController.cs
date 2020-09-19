using System.Collections;
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

    private void SetState(int state)
    {
        animator.SetInteger("State", state);
    }
    private void SetSpeedMultiplied(float value)
    {
        animator.SetFloat("Direction", value);
    }
    public void SetIdle()
    {
        SetState((int)STATES.IDLE);
        SetSpeedMultiplied(0);
    }

    public void SetWalk()
    {
        SetState((int)STATES.WALK);
        SetSpeedMultiplied(1);
    }

    public void SetRun()
    {
        SetState((int)STATES.RUN);
        SetSpeedMultiplied(1);
    }

    public void SetMelee()
    {
        SetState((int)STATES.MELEE);
        SetSpeedMultiplied(1);
    }
}
