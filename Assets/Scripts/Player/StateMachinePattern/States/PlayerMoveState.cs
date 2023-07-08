using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        private readonly int MoveHash = Animator.StringToHash("Move");
        
        public PlayerMoveState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
        }

        public override void Enter()
        {
            //Debug.Log("Enter Move State");
            
            Player.ChangePhysicMaterial(Player.DefaultMaterial);
            
            Player.JumpState.ResetAmountOfJumpsLeft();
            
            Player.PlayerAnimator.SetBool(MoveHash, true);
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
            else if (inputX == 0 && inputY == 0)
            {
                StateMachine.SwitchState(Player.IdleState);
            }

            if (inputX != 0 || inputY != 0)
            {
                Player.SetVelocityXZRaw(inputX, inputY, PlayerData.moveSpeed);
            }

            Player.CheckFlip(inputX);
        }

        public override void Exit()
        {
            Player.PlayerAnimator.SetBool(MoveHash, false);
        }
    }
}