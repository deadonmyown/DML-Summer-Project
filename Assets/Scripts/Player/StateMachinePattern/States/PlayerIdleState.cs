using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine stateMachine, Player player) : base(stateMachine, player)
        {
        }

        public override void Enter()
        {
            _player.Move(0);
            //TODO: Animation
        }

        public override void Tick()
        {
            if (_player.InputHandler.MovementInputX != 0)
            {
                _stateMachine.SwitchState(_player.MoveState);
            }
        }

        public override void Exit()
        {
            //TODO: Animation
        }
    }
}