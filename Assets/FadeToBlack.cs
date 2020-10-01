using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{

    public Image Image;
    public void FadeOut()
    {
        GetComponent<Canvas>().sortingOrder = 20;
        Image.GetComponent<Animator>().SetBool("Visible", false);
    }

    public void FadeIn()
    {
        GetComponent<Canvas>().sortingOrder = 0;
        Image.GetComponent<Animator>().SetBool("Visible", true);
    }
}
