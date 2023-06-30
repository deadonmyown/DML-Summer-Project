using System;
using Player.StateMachinePattern.States;
using UnityEngine;

namespace Player.StateMachinePattern
{
    public class PlayerStateMachine : StateMachine
    {
        public void Initialize(State startState)
        {
            SwitchState(startState);
        }
    }
}