using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{

    public Image Image;
    public void FadeOut()
    {
        Image.GetComponent<Animator>().SetBool("Visible", false);
    }

    public void FadeIn()
    {
        Image.GetComponent<Animator>().SetBool("Visible", true);
    }
}
