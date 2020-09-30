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
            FixX(xDiff);
        }
    }

    private float xDiff = 0;
    private void updateFacingSide()
    {
        animator.transform.rotation = Quaternion.LookRotation(flipX ? Vector3.back : Vector3.forward);
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
        animator.enabled = true;
        animator.SetInteger("State", (int)state);
        SetSpeedMultiplied(StateToSpeed[(int)state]);
        FixX();
    }
    private void FixX(float _xDiff = 0)
    {
        xDiff = _xDiff;
        animator.transform.localPosition = new Vector3((flipX ? -1 : 1) * xDiff, 0, 0);
    }
    private void SetSpeedMultiplied(float value)
    {
        animator.SetFloat("Direction", value);
    }
    public void SetIdle() { SetState(STATES.IDLE); }
    public void SetCharging() { SetState(STATES.IDLE); }
    public void SetStunned() { SetState(STATES.IDLE); }
    public void SetWalk() { SetState(STATES.WALK); }
    public void SetRun()
    {
        SetState(STATES.RUN);
        FixX(-0.82f);
    }
    public void SetMelee()
    {
        SetState(STATES.MELEE);
        FixX(1.35f);
    }
    public void SetHiding()
    {
        SetState(STATES.IDLE);
        animator.GetComponent<SpriteRenderer>().color = Color.black;
    }
    public void LeaveHiding()
    {
        animator.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetDirection(float direction)
    {
        SetSpeedMultiplied(direction);
    }
}
