using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        private readonly int IdleHash = Animator.StringToHash("Idle");
        
        public PlayerIdleState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Idle State");
            
            Player.ChangePhysicMaterial(Player.DefaultMaterial);
            
            //Player.ResetVelocity();
            Player.JumpState.ResetAmountOfJumpsLeft();

            Player.PlayerAnimator.SetBool(IdleHash, true);
        }

        public override void Tick()
        {
            var inputX = Player.InputHandler.MovementInputX;
            var inputY = Player.InputHandler.MovementInputY;
            var velocity = Player.Velocity.y;
            
            if (Player.InputHandler.JumpInput && Player.JumpState.CanJump() && Player.IsGrounded())
            {
                StateMachine.SwitchState(Player.JumpState);
            }
            else if (!Player.IsGrounded())
            {
                if (velocity > 0f)
                {
                    StateMachine.SwitchState(Player.InAirUpState);
                }
                else
                {
                    StateMachine.SwitchState(Player.InAirFallState);
                }
            }
            else if (inputX != 0 || inputY != 0)
            {
                StateMachine.SwitchState(Player.MoveState);
            }
        }

        public override void Exit()
        {
            Player.PlayerAnimator.SetBool(IdleHash, false);
        }
    }
}