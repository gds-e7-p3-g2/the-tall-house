using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorBool : MonoBehaviour
{
    [SerializeField] private string boolName = "myBool";

    public void SetBool(bool value)
    {
        gameObject.GetComponent<Animator>().SetBool(boolName, value);
    }
}
