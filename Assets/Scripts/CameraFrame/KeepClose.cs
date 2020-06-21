using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepClose : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] float MaxHorizontalDistance = 3f;
    [SerializeField] float MaxVerticalDistance = 20f;

    void FixedUpdate()
    {
        float xt = Target.transform.position.x;
        float yt = Target.transform.position.y;

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        float newX = Mathf.Max(Mathf.Min(x, xt + MaxHorizontalDistance), xt - MaxHorizontalDistance);
        float newY = Mathf.Max(Mathf.Min(y, yt + MaxVerticalDistance), yt - MaxVerticalDistance);

        transform.position = new Vector3(2f, 3f, z);
    }
}
