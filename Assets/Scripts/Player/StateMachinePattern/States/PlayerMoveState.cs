using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine stateMachine, Player player) : base(stateMachine, player)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            var inputX = _player.InputHandler.MovementInputX; 
            
            if (inputX == 0)
            {
                _stateMachine.SwitchState(_player.MoveState);
            }

            _player.Move(_player.MoveSpeed * inputX);
        }

        public override void Exit()
        {
        }
    }
}