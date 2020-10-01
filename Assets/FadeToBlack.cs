using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{

    public Image Image;
    public void FadeOut()
    {
        if (GetComponent<Canvas>() != null)
            GetComponent<Canvas>().sortingOrder = 20;
        Image.GetComponent<Animator>().SetBool("Visible", false);
    }

    public void FadeIn()
    {
        if (GetComponent<Canvas>() != null)
            GetComponent<Canvas>().sortingOrder = 0;
        Image.GetComponent<Animator>().SetBool("Visible", true);
    }
}
