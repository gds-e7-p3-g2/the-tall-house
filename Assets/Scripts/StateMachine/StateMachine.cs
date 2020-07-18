using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IStreamYouScream
{
    public abstract class StateMachine<TState> : MonoBehaviour where TState : State
    {
        public TState CurrentState { get; private set; }
        public void SetState(TState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = newState;
            CurrentState.Enter();

            // Debug.Log("NEW STATE " + CurrentState);
        }
    }
}