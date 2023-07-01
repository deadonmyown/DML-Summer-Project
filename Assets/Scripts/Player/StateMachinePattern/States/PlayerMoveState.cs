using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Move State");
            Player.JumpState.ResetAmountOfJumpsLeft();
        }

        public override void Tick()
        {
            var inputX = Player.InputHandler.MovementInputX; 
            
            if (Player.InputHandler.JumpInput && Player.JumpState.CanJump() && Player.IsGrounded())
            {
                StateMachine.SwitchState(Player.JumpState);
            }
            else if (!Player.IsGrounded())
            {
                StateMachine.SwitchState(Player.FallState);
            }
            if (inputX == 0)
            {
                StateMachine.SwitchState(Player.IdleState);
            }

            Player.Move(PlayerData.moveSpeed * inputX);
        }

        public override void Exit()
        {
        }
    }
}