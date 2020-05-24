using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHidePlayer : Action
{
    [SerializeField] PlayerController player;

    public override void PerformAction()
    {
        player.ToggleHiding();
    }
}