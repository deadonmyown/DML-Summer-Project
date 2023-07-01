using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Idle State");
            Player.StopMove();
            Player.JumpState.ResetAmountOfJumpsLeft();
            //TODO: Animation
        }

        public override void Tick()
        {
            if (Player.InputHandler.JumpInput && Player.JumpState.CanJump() && Player.IsGrounded())
            {
                StateMachine.SwitchState(Player.JumpState);
            }
            else if (!Player.IsGrounded())
            {
                StateMachine.SwitchState(Player.FallState);
            }
            else if (Player.InputHandler.MovementInputX != 0)
            {
                StateMachine.SwitchState(Player.MoveState);
            }
        }

        public override void Exit()
        {
            //TODO: Animation
        }
    }
}