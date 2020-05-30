using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHidePlayer : Action {
    public override void PerformAction () {
        GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().ToggleHiding ();
    }
}