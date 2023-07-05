using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerInAirUpState : PlayerBaseState
    {
        private readonly int JumpHash = Animator.StringToHash("Jump");
        
        public PlayerInAirUpState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enter In Air UP State");
            
            Player.ChangePhysicMaterial(Player.SlipperMaterial);
            
            Player.JumpState.DecreaseAmountOfJumpsLeft();
            
            Player.PlayerAnimator.SetBool(JumpHash, true);
            
        }

        public override void Tick()
        {
            var inputX = Player.InputHandler.MovementInputX;
            var inputY = Player.InputHandler.MovementInputY;

            var velocity = Player.Velocity;
            
            if (Player.InputHandler.JumpInput && Player.JumpState.CanJump())
            {
                StateMachine.SwitchState(Player.JumpState);
            }
            else if (velocity.y <= 0f)
            {
                StateMachine.SwitchState(Player.FallState);
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
            Player.PlayerAnimator.SetBool(JumpHash, false);
        }
    }
}