using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoSetInactive : MonoBehaviour
{
    public float cooldown = 2f;
    public UnityEvent OnDeactivated;
    void OnEnable()
    {
        Invoke("SetInactive", cooldown);
    }
    private void SetInactive()
    {
        OnDeactivated.Invoke();
        gameObject.SetActive(false);
    }
}
