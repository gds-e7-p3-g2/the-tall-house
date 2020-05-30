using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTeleport : Action
{
    private GameObject Player;
    [SerializeField] GameObject Destination;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    public override void PerformAction()
    {
        Player.transform.position = new Vector3(Destination.transform.position.x, Destination.transform.position.y, Player.transform.position.z);
    }
}
