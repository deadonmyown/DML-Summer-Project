using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public abstract class PlayerBaseState : State
    {
        protected readonly PlayerStateMachine _stateMachine;
        protected readonly Player _player;
        protected PlayerBaseState(PlayerStateMachine stateMachine, Player player)
        {
            _stateMachine = stateMachine;
            _player = player;
        }
        
        //TODO: Basic Methods for player
    }
}