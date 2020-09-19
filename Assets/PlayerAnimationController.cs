using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator { get { return gameObject.GetComponent<Animator>(); } }

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
