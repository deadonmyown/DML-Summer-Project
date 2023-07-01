using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerFallState : PlayerBaseState
    {
        private bool coyoteTime;
        
        public PlayerFallState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Fall State");
            coyoteTime = true;
        }

        public override void Tick()
        {
            CheckCoyoteTime();
            
            var inputX = Player.InputHandler.MovementInputX;

            if (Player.InputHandler.JumpInput && Player.JumpState.CanJump() && coyoteTime)
            {
                StateMachine.SwitchState(Player.JumpState);
            }

            if (Player.IsGrounded())
            {
                StateMachine.SwitchState(Player.IdleState);
            }
            
            Player.Move(PlayerData.moveSpeed * inputX);
        }

        public override void Exit()
        {
        }

        private void CheckCoyoteTime()
        {
            if (Time.time > StartTime + PlayerData.coyoteTime)
            {
                coyoteTime = false;
                Player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }
    }
}