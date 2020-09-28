using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithPositionBoundries : MonoBehaviour
{
    public GameObject TopRight;
    public GameObject BottomLeft;

    void Update()
    {
        CheckBoundries();
    }

    void OnDrawGizmos()
    {
        if (TopRight == null || BottomLeft == null)
        {
            return;
        }
        Gizmos.color = new Color(1, 1, 1, 0.1f);
        Gizmos.DrawWireCube((TopRight.transform.position + BottomLeft.transform.position) / 2, TopRight.transform.position - BottomLeft.transform.position);
    }

    private void CheckBoundries()
    {
        if (BottomLeft == null || TopRight == null)
        {
            return;
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, BottomLeft.transform.position.x, TopRight.transform.position.x);
        pos.y = Mathf.Clamp(pos.y, BottomLeft.transform.position.y, TopRight.transform.position.y);
        transform.position = pos;
    }
}
