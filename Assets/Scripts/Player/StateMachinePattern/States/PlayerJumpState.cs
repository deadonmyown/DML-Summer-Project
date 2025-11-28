using UnityEngine;

namespace Player.StateMachinePattern.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        private readonly int _jumpHash = Animator.StringToHash("Jump");
        private int _amountOfJumpsLeft;
        
        public PlayerJumpState(PlayerStateMachine stateMachine, Player player, PlayerData playerData) : base(stateMachine, player, playerData)
        {
            
        }

        public override void Enter()
        {
            //Debug.Log("Enter Jump State");
            
            Player.ChangePhysicMaterial(Player.SlipperMaterial);
            
            Player.SetVelocityY(PlayerData.jumpForce);
            _amountOfJumpsLeft--;
            Player.InputHandler.StopJump();

            Player.PlayerAnimator.SetBool(_jumpHash, true);
        }

        public override void Tick()
        {
            CheckJumpMultiplier();
            CheckEndJumpInput();
            
            var velocity = Player.Velocity.y;

            var inputX = Player.InputHandler.MovementInputX;
            var inputY = Player.InputHandler.MovementInputY;
            if (Player.InputHandler.JumpInput && CanJump())
            {
                Player.SetVelocityY(PlayerData.jumpForce);
                _amountOfJumpsLeft--;
                Player.InputHandler.StopJump();
            }
            else if (velocity <= 0f)
            {
                StateMachine.SwitchState(Player.FallState);
            }
            
            Player.SetVelocityXZRaw(inputX, inputY, PlayerData.jumpMoveSpeed);
            Player.CheckFlip(inputX);
        }

        public override void Exit()
        {
            Player.PlayerAnimator.SetBool(_jumpHash, false);
        }
        
        public bool CanJump() => _amountOfJumpsLeft > 0 ? true : false;

        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;

        private void CheckJumpMultiplier()
        {
            if (Player.InputHandler.JumpInputStop)
            {
                Player.SetVelocityY(Player.Velocity.y * PlayerData.jumpMultiplier);
            }
        }

        private void CheckEndJumpInput()
        {
            if (!Player.InputHandler.JumpInputStart)
            {
                Player.SetVelocityY(Player.Velocity.y * PlayerData.softJumpMultiplier);
            }
        }
    }
}