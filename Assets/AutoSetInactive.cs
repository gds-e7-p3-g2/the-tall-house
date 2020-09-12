using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetInactive : MonoBehaviour
{
    void OnDisable()
    {
    }

    void OnEnable()
    {
        Invoke("SetInactive", 2f);
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
