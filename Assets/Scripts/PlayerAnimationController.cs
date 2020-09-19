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
        Debug.Log("STATE " + state);
        animator.enabled = true;
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
