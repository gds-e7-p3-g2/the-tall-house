using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IStreamYouScream
{
    public class DenyButtonListener : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (InputManager.MenuDeny)
            {
                gameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
