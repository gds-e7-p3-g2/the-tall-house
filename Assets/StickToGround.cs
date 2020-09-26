using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToGround : MonoBehaviour
{
    public float DesiredDistance = 3.1666f;
    void Start()
    {
        StickToTheGround();
    }

    public void StickToTheGround()
    {
        int layerMask = 1 << 11;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layerMask);

        gameObject.transform.position -= new Vector3(0, hit.distance - DesiredDistance, 0);
    }
}
