using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithPositionBoundries : MonoBehaviour
{
    public GameObject TopLeft;
    public GameObject BottomRight;

    // Update is called once per frame
    void Update()
    {
        CheckBoundries();
    }

    void OnDrawGizmos()
    {
        if (TopLeft == null || BottomRight == null)
        {
            return;
        }
        Gizmos.color = new Color(1, 1, 1, 0.2f);
        Gizmos.DrawCube((TopLeft.transform.position + BottomRight.transform.position) / 2, TopLeft.transform.position - BottomRight.transform.position);

    }

    private void CheckBoundries()
    {
        if (TopLeft == null || BottomRight == null)
        {
            return;
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, TopLeft.transform.position.x, BottomRight.transform.position.x);
        // pos.y = Mathf.Clamp(pos.y, TopLeft.transform.position.y, BottomRight.transform.position.y);
        transform.position = pos;
    }
}
