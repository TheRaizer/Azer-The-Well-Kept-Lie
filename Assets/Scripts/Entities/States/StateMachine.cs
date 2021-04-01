using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.States
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        private Dictionary<Type, State> availableStates;

        public void SetStates(Dictionary<Type, State> _availableStates)
        {
            availableStates = _availableStates;
        }

        public void Initialize(Type startingState)
        {
            CurrentState = availableStates[startingState];
            CurrentState.Enter();
        }

        public void ChangeState(Type nextState)
        {
            CurrentState.Exit();
            CurrentState = availableStates[nextState];
            CurrentState.Enter();
        }
    }
}
