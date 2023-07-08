using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerFallState : PlayerBaseState
    {
        private readonly int FallHash = Animator.StringToHash("Fall");

        public PlayerFallState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            //Debug.Log("Enter Fall State");
            
            Player.ChangePhysicMaterial(Player.SlipperMaterial);
            
            Player.PlayerAnimator.SetBool(FallHash, true);
            
            Player.AddVelocityY(PlayerData.fallingGravityScale);
        }

        public override void Tick()
        {
            var inputX = Player.InputHandler.MovementInputX;
            var inputY = Player.InputHandler.MovementInputY;

            if (Player.InputHandler.JumpInput && Player.JumpState.CanJump())
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
    }
}