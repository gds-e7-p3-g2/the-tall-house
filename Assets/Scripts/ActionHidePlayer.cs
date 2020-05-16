using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHidePlayer : Action
{
    [SerializeField] PlayerHiding playerHiding;

    public override void PerformAction()
    {
        Debug.Log("Hiding Player");
        if (playerHiding.GetIsHiding())
        {
            playerHiding.LeaveHideout();
        }
        else
        {
            playerHiding.EnterHideout();
        }
    }
}