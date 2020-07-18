using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public abstract class State
    {
        public virtual void Enter()
        {
            Debug.LogWarning("State:Enter not implemented");
        }

        public virtual void Exit()
        {
            Debug.LogWarning("State:Exit not implemented");
        }
    }
}