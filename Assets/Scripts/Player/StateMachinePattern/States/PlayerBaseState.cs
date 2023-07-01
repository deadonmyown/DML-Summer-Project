using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public abstract class PlayerBaseState : State
    {
        protected readonly PlayerStateMachine StateMachine;
        protected readonly Player Player;
        protected readonly PlayerData PlayerData;
        protected float StartTime;
        
        protected PlayerBaseState(PlayerStateMachine stateMachine, Player player, PlayerData playerData)
        {
            StateMachine = stateMachine;
            Player = player;
            PlayerData = playerData;
        }

        public override void Enter()
        {
            StartTime = Time.time;
        }

        //TODO: Basic Methods for player
    }
}