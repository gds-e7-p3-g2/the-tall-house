using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationController : MonoBehaviour
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

    public void MarkDead()
    {
        animator.SetBool("Alive", false);
    }
}
