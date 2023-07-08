using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerInAirFallState : PlayerBaseState
    {
        private readonly int FallHash = Animator.StringToHash("Fall");
        private bool coyoteTime;
        
        public PlayerInAirFallState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("Enter In Air FALL State");
            
            coyoteTime = true;
            
            Player.ChangePhysicMaterial(Player.SlipperMaterial);
            
            Player.PlayerAnimator.SetBool(FallHash, true);
            
            //Player.AddVelocityY(PlayerData.fallingGravityScale);
        }

        public override void Tick()
        {
            CheckCoyoteTime();
            
            var inputX = Player.InputHandler.MovementInputX;
            var inputY = Player.InputHandler.MovementInputY;

            if (Player.InputHandler.JumpInput && Player.JumpState.CanJump() && coyoteTime)
            {
                StateMachine.SwitchState(Player.JumpState);
            }
            else if (Player.IsGrounded())
            {
                StateMachine.SwitchState(Player.IdleState);
            }
            
            Player.SetVelocityXZRaw(inputX, inputY, PlayerData.jumpMoveSpeed);
            Player.CheckFlip(inputX);
        }

        public override void Exit()
        {
            Player.PlayerAnimator.SetBool(FallHash, false);
        }

        private void CheckCoyoteTime()
        {
            if (Time.time > StartTime + PlayerData.coyoteTime)
            {
                //Debug.Log("Coyote time left");
                coyoteTime = false;
                Player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }
    }
}