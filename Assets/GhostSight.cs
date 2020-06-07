using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GhostSight : MonoBehaviour
{

    [SerializeField] Light2D[] lighs;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player.GetIsHiding())
        {
            return;
        }
        foreach (Light2D light in lighs)
        {
            light.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        foreach (Light2D light in lighs)
        {
            light.color = Color.blue;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player.GetIsHiding())
        {
            return;
        }
        foreach (Light2D light in lighs)
        {
            light.color = Color.red;
        }
    }
}
