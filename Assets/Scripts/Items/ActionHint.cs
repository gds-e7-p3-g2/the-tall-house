﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public class ActionHint : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActiveRecursively(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}