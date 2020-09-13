using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fame_popup_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private Image customImage;
    
    void onTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            customImage.enabled = true;
        }
    }
    
    void onTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            customImage.enabled = false;
        }
    }

  
}
